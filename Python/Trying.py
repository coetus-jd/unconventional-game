from sre_constants import SUCCESS
import cv2
import time
import numpy as np
import HandTrackingModule as htm
import math
import socket

UDP_IP = "127.0.0.1"
UDP_PORT = 7777

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

wCam, hCam = 640, 480

cap = cv2.VideoCapture(0)
cap.set(3, wCam)
cap.set(4, wCam)

pTime = 0

detector = htm.handDetector(detectionCon=0.75)

tipIds = [4, 8, 12, 16, 20]

while True:
    success, img = cap.read()
    img = detector.findHands(img)
    lmList = detector.findPosition(img, draw=False)

    if len(lmList) != 0:
        fingers = []

        #thumb
        if lmList[tipIds[0]][1] > lmList[tipIds[0] - 1][1]:
            fingers.append(1)
        else:
            fingers.append(0)
        
        # 4 Fingers
        for id in range(1,5):
            if lmList[tipIds[id]][2] < lmList[tipIds[id] - 2][2]:
                fingers.append(1)
            else:
                fingers.append(0)

        totalFingers = fingers.count(1)
        #print(totalFingers)

        if(totalFingers == 2):
            sock.sendto( ("UP").encode(), (UDP_IP, UDP_PORT) )
        if(totalFingers == 3):
            sock.sendto( ("DOWN").encode(), (UDP_IP, UDP_PORT) )

    cTime = time.time()
    fps = 1/(cTime - pTime)
    pTime = cTime

    cv2.putText(img, f'FPS: {int(fps)}', (400,70), cv2.FONT_HERSHEY_PLAIN, 3, (255, 0, 0), 3)

    cv2.imshow("Image",img)
    cv2.waitKey(1)
    