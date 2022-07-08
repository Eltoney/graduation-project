from select import select
import selectors
import socket
import types
import sys
from predication import Predication

from threadHandler import TaskHandler

class Server:
    ACCEPT_CONNECT=1
    READ_CHANNEL=2
    def __init__(self,numberOfOperation) :
        self.numberOfOperation=numberOfOperation
        self.selector = selectors.DefaultSelector()
        Predication()
        

    def connect(self):
    
        self.serverSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.serverSocket.bind(('127.0.0.1', 41355))
        self.serverSocket.listen(100)
        self.serverSocket.setblocking(False)
        self.selector.register(self.serverSocket, selectors.EVENT_READ, data=(1))
        print("Server Started")
        self.select()

    def handleClient(self,channel):
            socket,addr=channel.accept()
            print("Accept: ",addr)
            socket.setblocking(False)
            self.selector.register(socket, selectors.EVENT_READ, data=(2))
    def handlRead(self,channel):
        data= channel.recv(1024)
        if data:
            string=str(data.decode())
            TaskHandler().addTask(string)
        channel.close()
        self.selector.unregister(channel)
    def select(self):
        while True:
            selection= self.selector.select()
        
            for key,mask in selection:
                print(key)
                type=key.data
                
                if type == 1:
                    self.handleClient(key.fileobj)
                else:
                    self.handlRead(key.fileobj)


if __name__ == '__main__':
    numberOfOperation=1

    a=  Server(numberOfOperation)
    a.connect()
  