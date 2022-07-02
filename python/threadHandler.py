import queue
import json
from re import S
from threading import Thread
import pymssql
import database
from singleton import Singleton
class TaskHandler(metaclass=Singleton):
    def __init__(self) -> None:
        self.database=database.Database()
        print("New Worker")
        self.worker=False
        self.taksQueue=queue.SimpleQueue()
    def addTask(self,taskJson):
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
        self.database.updateTask(task['TaskID'],1)
        #handle result here
        result=1 #change to its result
        if result !=-1:
            self.database.updateTask(task['TaskID'],2,result=result)
        else:
            self.database.updateTask(task['TaskID'],3,result=result)
    
