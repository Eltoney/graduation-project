from singleton import Singleton
import pymssql
class Database(metaclass=Singleton):
    
    def __init__(self) -> None:
        self.connection=pymssql.connect(host="DESKTOP-7M1T1OA",user="DESKTOP-7M1T1OA\Ali Elmorsy",password="01021624394",database="GraduateProjectDatabase")

    def updateTask(self,taskID,taskStatus,result=-1):
        cursor=self.connection.cursor()
        cursor.execute("update tasks set currentState = %d, result=%d where taskID=%d",(taskStatus,result,taskID))
        cursor.close()

        