from tensorflow import keras
import numpy as np
from tensorflow import expand_dims
from PIL import Image,ImageOps


from singleton import Singleton
from tensorflow import keras
import tensorflow as tf
class Predication(metaclass=Singleton):
    def __init__(self) -> None:
        self.model = keras.models.load_model("../FinalOutput")

    def predictAge(self, image,gender):
        print("Predication")
        if gender==-1:
            male=self.predictAge(image,1)
            female=self.predictAge(image,0)
            return (male+female)/2
        try:
            im=Image.open(image)
            im=ImageOps.grayscale(im)
            im = im.resize((600,600))
            imArray=np.asfarray(im)
            imArray = tf.cast(imArray, tf.float32) / 255.0
            imArray=tf.expand_dims(imArray, axis = 0)
            gender=np.asarray(gender).astype(int)
            gender=tf.expand_dims(gender, axis = 0)
            inputArray=(imArray,gender)
            result=self.model.predict(inputArray)
            return result[0][0]
        except:
            return -1
