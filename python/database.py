from singleton import Singleton
import pyodbc

class Database(metaclass=Singleton):
    
    def __init__(self) -> None:
        self.connection = pyodbc.connect('Driver={SQL Server};'
                      'Server=DESKTOP-NHABO0O;'
                      'Database=xray;'
                      'Trusted_Connection=yes;')
    def updateTask(self, taskID, taskStatus, result=-1):
        cursor=self.connection.cursor()
        cursor.execute(f"update tasks set currentState = {taskStatus}, result={result} where taskID={taskID}")
        cursor.commit()
        cursor.close()

        