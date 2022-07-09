import queue
import json
from re import S
from statistics import mode
from threading import Thread
import database
from singleton import Singleton
from predication import Predication
class TaskHandler(metaclass=Singleton):
    def __init__(self) -> None:
        ## Singleton resultGenerator Here
        self.database=database.Database()
        self.model=Predication()

        print("New Worker")
        self.worker=False
        self.taksQueue=queue.SimpleQueue()
    def addTask(self,taskJson):
        print(taskJson)
        j=json.loads(taskJson)
        self.taksQueue.put(j)
        if not self.worker:
            Thread(target=self.startQueue,daemon=True).start()

    def startQueue(self):
        print("Started")
        self.worker=True
        while self.taksQueue.qsize() >0:
         self.getTaskAndExecute()
        self.worker=False
    
    def getTaskAndExecute(self):
        task=self.taksQueue.get()
        self.database.updateTask(task['taskID'],1)

        #handle result here
        result=self.model.predictAge(task['imageLocation'],task["gender"]) #change to its result
        if result !=-1:
            self.database.updateTask(task['taskID'],2,result=result)
        else:
            self.database.updateTask(task['taskID'],3,result=result)
    
