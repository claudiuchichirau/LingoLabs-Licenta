from flask import Flask, request
from flask_cors import CORS
from tensorflow.keras.models import load_model
from imutils.contours import sort_contours
import io
import imutils
import traceback
import cv2
import cv2 as cv
import numpy as np
# import tensorflow as tf
import matplotlib.pyplot as plt
import matplotlib as mpl
import pandas as pd
# import pytesseract


app = Flask(__name__)
CORS(app)

@app.route('/upload-image', methods=['POST'])
def upload_image():
    try:
        image_bytes = request.data  # Get the byte array from the request
        image_np = np.frombuffer(image_bytes, dtype=np.uint8)
        image = cv2.imdecode(image_np, cv2.IMREAD_COLOR)
        cv2.imwrite('uploaded_image.jpg', image)  # Save the image as 'uploaded_image.jpg'

        try:
            textLines=lineSegment(image)
            print ('No. of Lines',len(textLines))
            wordsList = wordSegment(textLines)
            print ('No. of Words',len(wordsList))
            counter = 0
            for word in wordsList:
                print ('LetterGray shape: ',word.shape)
                gray = cv.cvtColor(word, cv.COLOR_BGR2GRAY)
                th, word = cv.threshold(gray, 127, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
                # letterGray = fitToSize(word)
                letter2 = word.copy()
                word = cv.dilate(word,None,iterations = 4)

                h = word.shape[0]
                w = word.shape[1]
                
                upoints, dpoints = findCapPoints(word)        
                meanu, lb = baselines(letter2, upoints, dpoints, h, w)
                
        ##-----------Final Baseline row numbers-----------------------####
        #       Ignore all points avove and below these rows 
                upper_baseline = meanu
                lower_baseline = lb
                
        ##--------------------Make histogram-------------------------------------###   
                
                colcnt = histogram(letter2, upper_baseline, lower_baseline, w)
                print('\n\nColcnt: ',colcnt)
        ###------------------------Visualize segmentation------------------------------#####        
                ## Tuning Parameters
                min_pixel_threshold = 30
                min_separation_threshold = 27
                min_round_letter_threshold = 190
                
                seg = visualize(letter2, upper_baseline, lower_baseline, min_pixel_threshold, min_separation_threshold, min_round_letter_threshold, colcnt, word, h)
                wordImgList = segmentCharacters(seg,word)
                for i in wordImgList:
                    cv.imwrite("./result/characters/" + str(counter) +".jpeg",i)
                    counter=counter+1
                
        ###---------------------------------------------------------------------------#####        
                
            print('Original Image')         
            plt.imshow(img)
            plt.show()

        except Exception as e:
            print ('Error Message ',e)
            cv.destroyAllWindows()
            traceback.print_exc()
            pass

        traceback.print_exc() 

        return 'Image received and processed successfully', 200
    except Exception as ex:
        return str(ex), 500  # Return an error message if something goes wrong


def lineSegment(img):
    gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
    th, threshed = cv.threshold(gray, 127, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
   
    upper=[]
    lower=[]
    flag=True
    for i in range(threshed.shape[0]):

        col = threshed[i:i+1,:]
        cnt=0
        if flag:
            cnt=np.count_nonzero(col == 255)
            if cnt >0:
                upper.append(i)
                flag=False
        else:
            cnt=np.count_nonzero(col == 255)
            if cnt < 7:
                lower.append(i)
                flag=True
    textLines=[]
    if len(upper)!= len(lower):lower.append(threshed.shape[0])
    # print("\n\nupper: ", upper)
    # print("\n\nlower: ", lower)
    for i in range(len(upper)):
        timg=img[upper[i]:lower[i],0:]
        
        if timg.shape[0]>5:
            # plt.imshow(timg)
            # plt.show()
            timg=cv.resize(timg,((timg.shape[1]*5,timg.shape[0]*8)))
            textLines.append(timg)

    return textLines

def wordSegment(textLines, max_word_spacing=30):
    wordImgList=[]
    counter=0
    cl=0

    try:
        for txtLine in textLines:
            gray = cv.cvtColor(txtLine, cv.COLOR_BGR2GRAY)
            th, threshed = cv.threshold(gray, 100, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
            final_thr = cv.dilate(threshed,None,iterations = 20)

            #plt.imshow(final_thr)
            #plt.show()
            
            contours, hierarchy = cv.findContours(final_thr,cv.RETR_EXTERNAL,cv.CHAIN_APPROX_SIMPLE)
            boundingBoxes = [cv.boundingRect(c) for c in contours]
            (contours, boundingBoxes) = zip(*sorted(zip(contours, boundingBoxes), key=lambda b: b[1][0], reverse=False))

            prev_x, prev_w, prev_contour = None, None, None
            new_contours = []
            for cnt in contours:
                area = cv.contourArea(cnt)
                x, y, w, h = cv.boundingRect(cnt)
                if area > 10000:
                    if prev_x is not None:
                        # Calculează distanța dintre contururile consecutive
                        spacing = x - (prev_x + prev_w)
                        if spacing < max_word_spacing:
                            # Extrage coordonatele conturului anterior
                            prev_contour = new_contours.pop()
                            prev_x, prev_y, prev_w, prev_h = cv.boundingRect(prev_contour)
                            # Actualizează coordonatele și dimensiunile conturului curent
                            x = min(x, prev_x)
                            y = max(y, prev_y)
                            w = w + prev_w
                            h = max(h, prev_h)
                            # Creăm un nou contur folosind coordonatele actualizate
                            new_contour = np.array([[[x, y]], [[x + w, y]], [[x + w, y + h]], [[x, y + h]]])
                            # Adaugă noul contur concatenat în lista new_contours
                            new_contours.append(new_contour)

                            prev_x, prev_w = x, w
                            prev_contour = new_contour
                        else:
                            new_contours.append(cnt)
                            xx, yy, ww, hh = cv.boundingRect(cnt)
                            prev_x, prev_w = xx, ww
                    else:
                        new_contours.append(cnt)
                        prev_x, prev_w = x, w
                        prev_contour = cnt


            
            for cnt in new_contours:
                area = cv.contourArea(cnt)
                x, y, w, h = cv.boundingRect(cnt)

                if area > 10000:
                    letterBgr = txtLine[0:txtLine.shape[1], x:x + w]
                    wordImgList.append(letterBgr)

                    #plt.imshow(letterBgr)
                    #plt.show()

                    cv.imwrite("./result/words/" + str(counter) + ".jpg", letterBgr)
                    counter += 1
            cl += 1
        
        return wordImgList
    except Exception as e:
            print ('Error Message ',e)

def fitToSize(thresh1):
    mask = thresh1 > 0
    coords = np.argwhere(mask)

    x0, y0 = coords.min(axis=0)
    x1, y1 = coords.max(axis=0) + 1   # slices are exclusive at the top
    cropped = thresh1[x0:x1,y0:y1]
    return cropped

def findCapPoints(img):
    cpoints=[]
    dpoints=[]
    for i in range(img.shape[1]):
        col = img[:,i:i+1]
        k = col.shape[0]
        while k > 0:
            if col[k-1]==255:
                dpoints.append((i,k))
                break
            k-=1
        
        for j in range(col.shape[0]):
            if col[j]==255:
                cpoints.append((i,j))
                break
    return cpoints,dpoints

def baselines(letter2, upoints, dpoints, h, w):
    ##-------------------------Creating upper baseline-------------------------------##
    try:
        colu = []
        for i in range(len(upoints)):
            colu.append(upoints[i][1])
        
        maxyu = max(colu)
        minyu = min(colu)
        avgu = (maxyu + minyu) // 2
        meanu = np.around(np.mean(colu)).astype(int)
        print('Upper:: Max, min, avg, mean:: ',maxyu, minyu, avgu, meanu)
        
        ##-------------------------------------------------------------------------------##
        ##-------------------------Creating lower baseline process 1--------------------------##
        cold = []
        for i in range(len(dpoints)):
            cold.append(dpoints[i][1])
        
        maxyd = max(cold)
        minyd = min(cold)
        avgd = (maxyd + minyd) // 2
        meand = np.around(np.mean(cold)).astype(int)
        print('Lower:: Max, min, avg, mean:: ',maxyd, minyd, avgd, meand)
        
        ##-------------------------------------------------------------------------------##
        ##-------------------------Creating lower baseline process 2---------------------------##
        cn = []
        count = 0

        for i in range(h):
            for j in range(w):
                if(letter2[i,j] == 255):
                    count+=1
            if(count != 0):
                cn.append(count)
                count = 0    
        maxindex = cn.index(max(cn))
        print('Max pixels at: ',maxindex)
        
        ##------------------Printing upper and lower baselines-----------------------------##
        
        cv.line(letter2,(0,meanu),(w,meanu),(255,0,0),2)
        lb = 0
        if(maxindex > meand):
            lb = maxindex
            cv.line(letter2,(0,maxindex),(w,maxindex),(255,0,0),2)
        else:
            lb = meand
            cv.line(letter2,(0,meand),(w,meand),(255,0,0),2)
            
        plt.imshow(letter2)
        plt.show()
        return meanu, lb
    except Exception as ex:
        print('Error in baseline calculation' + str(ex))
        pass

def histogram(letter2, upper_baseline, lower_baseline, w):
    ##------------Making Histograms (Default)------------------------######
    cropped = letter2[upper_baseline:lower_baseline,0:w]
    plt.imshow(cropped)
    plt.show()
    colcnt = np.sum(cropped==255, axis=0)
    x = list(range(len(colcnt)))
    plt.plot(colcnt)
    plt.fill_between(x, colcnt, 1, facecolor='blue', alpha=0.5)
    plt.show()  
    return colcnt     

def visualize(letter2, upper_baseline, lower_baseline, min_pixel_threshold, min_separation_threshold, min_round_letter_threshold, colcnt, letterGray, h):
    seg = []
    seg1 = []
    seg2 = []
    ## Check if pixel count is less than min_pixel_threshold, add segmentation point
    for i in range(len(colcnt)):
      if(colcnt[i] > min_pixel_threshold):
         print('Am adaugat in sg1 coloana',i,' cu colcnt: ', colcnt[i])
         seg1.append(i)

    print('\n\nseg1 is: ', seg1)
          
    ## Check if 2 consequtive seg points are greater than min_separation_threshold in distance
    for i in range(len(seg1)-1):
        if(seg1[i+1]-seg1[i] > min_separation_threshold):
            seg2.append(seg1[i])
        # print('At seg2 where i: ', i, ' the difference is: ', seg1[i+1]-seg1[i])

    print('\n\nseg2 is: ', seg2)

    ##------------Modified segmentation for removing circles----------------------------###            
    arr=[]
    for i in (seg2):
        arr1 = []
        j = upper_baseline
        while(j <= lower_baseline):
            if(letterGray[j,i] == 255):
                arr1.append(1)
            else:
                arr1.append(0)
            j+=1
        arr.append(arr1)
    print('At arr Seg here: ', seg2)
    
    ones = []
    for i in (arr):
        ones1 = []
        for j in range(len(i)):
            if (i[j] == 1):
                ones1.append([j])
        ones.append(ones1)
    
    diffarr = []
    for i in (ones):
        diff = i[len(i)-1][0] - i[0][0]
        diffarr.append(diff)
    print('Difference array: ',diffarr)
    
    for i in range(len(seg2)):
        if(diffarr[i] < min_round_letter_threshold):
            seg.append(seg2[i])
    ##---------------------------------------------------------------------------##
    ## Make the Cut 
    for i in range(len(seg)):
        letter3 = cv.line(letter2,(seg[i],0),(seg[i],h),(255,0,0),2)
    
    print("Does it work::::")
    plt.imshow(letter3)
    plt.show()
    return seg 

def segmentCharacters(seg,lettergray):
    s=0
    wordImgList = []
    fn = 0
    for i in range(len(seg)):
        if i==0:
            s=seg[i]
            if s > 15:
                wordImg = lettergray[0:,0:s]
                cntx=np.count_nonzero(wordImg == 255) 
                print ('count',cntx)
                plt.imshow(wordImg)
                plt.show()
                fn=fn+1
            else:
                continue
        elif (i != (len(seg)-1)):
            if seg[i]-s > 15:
                wordImg = lettergray[0:,s:seg[i]]
                cntx=np.count_nonzero(wordImg == 255) 
                print ('count',cntx)
                plt.imshow(wordImg)
                plt.show()
                fn=fn+1
                s=seg[i]
            else:
                continue
        else:
            wordImg = lettergray[0:,seg[len(seg)-1]:]
            cntx=np.count_nonzero(wordImg == 255) 
            print ('count',cntx)
            plt.imshow(wordImg)
            plt.show()
            fn=fn+1
        wordImgList.append(wordImg)

    return wordImgList

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5100)
