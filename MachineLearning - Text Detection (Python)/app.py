from flask import Flask, request, jsonify
from flask_cors import CORS
from pathlib import Path
import io
import os
import traceback
import cv2
import cv2 as cv
import numpy as np
import matplotlib.pyplot as plt
import requests
import urllib.parse
from openai import OpenAI



app = Flask(__name__)
CORS(app)

@app.route('/upload-image', methods=['POST'])
def upload_image():
    try:
        clean_directory("./result/characters")
        clean_directory("./result/words")
        
        try:
            # Remove the uploaded image file
            if os.path.exists("uploaded_image.jpg"):
                os.remove("uploaded_image.jpg")
        except Exception as e:
            print ('Error Message ',e)
            return {'message': f'Error occurred: {e}'}, 500
            pass

        lesson_requirement = request.form['lessonRequirement']
        image_file = request.files['image']
        image_bytes = image_file.read()
        image_np = np.frombuffer(image_bytes, dtype=np.uint8)
        image = cv2.imdecode(image_np, cv2.IMREAD_COLOR)
        cv2.imwrite('uploaded_image.jpg', image)  # Save the image as 'uploaded_image.jpg'

        try:
            text_lines = detect_text_lines(image)
            words_list = detect_words(text_lines)
            wordCounter = 0
            for word in words_list:
                gray = cv.cvtColor(word, cv.COLOR_BGR2GRAY)
                th, word = cv.threshold(gray, 127, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
                copy_letter = word.copy()
                word = cv.dilate(word,None,iterations = 4)

                h = word.shape[0]
                w = word.shape[1] 
                
                column_count = calc_histogram(copy_letter, w)
                # print('\n\nColcnt: ',colcnt)

                min_pixel_threshold = 29
                min_separation_threshold = 10
                min_round_letter_threshold = 190
                
                segment_points = detect_characters_lines(copy_letter, min_pixel_threshold, min_separation_threshold, min_round_letter_threshold, column_count, word, h)
                characters_list = characters_segmentation(segment_points, word)

                wordFolder = f"./result/characters/word-{wordCounter}"
                os.makedirs(wordFolder, exist_ok=True)

                counter = 0
                for i in characters_list:
                    # Inversează culorile imaginii
                    inverted_image = cv.bitwise_not(i)
                    cv.imwrite(os.path.join(wordFolder, f"char-{counter}.jpeg"), inverted_image)
                    counter += 1
                wordCounter += 1
                
        ###---------------------------------------------------------------------------#####        

        except Exception as e:
            print ('Error Message ',e)
            cv.destroyAllWindows()
            traceback.print_exc()
            return {'message': f'Error occurred: {e}'}, 500
            pass

        traceback.print_exc() 

        try:
            final_string = ""
            characters_dir = "D:/Folder Claudiu/Facultate FII UAIC/Anul 3 (2023 - 2024)/Licență/LingoLabs-Licenta/MachineLearning - Text Detection (Python)/result/characters"

            for wordFolder in os.listdir("./result/characters"):
                word_dir = os.path.join(characters_dir, wordFolder)
                for filename in os.listdir(word_dir):
                    image_path = os.path.join(characters_dir, wordFolder, filename)
                    url = f"https://localhost:56896/predict?imagePath={urllib.parse.quote(image_path)}"
                    response = requests.post(url, verify=False)

                    if response.status_code == 200:
                        result = response.json()
                        letter = result["predictedLabel"]
                        final_string += letter
                    else:
                        print(f"Error: {response.status_code}")
                final_string += " "

            print('\n\n\t\tFINAL STRING: ', final_string)

            corrected_text, final_score, score_explanation = openAi_correct_text(final_string, lesson_requirement)

            # print('\n\n\t\tURMEAZA SA TRIMIT:\t', corrected_text, '\n\n\t\t', final_score, '\n\n\t\t', score_explanation)

            return jsonify({'corrected_text': corrected_text, 'final_score': final_score, 'score_explanation': score_explanation}), 200
        except Exception as e:
            print ('Error Message ',e)
            return {'message': f'Error occurred: {e}'}, 500
            pass
    except Exception as ex:
        print ('Error Message ',ex)
        return str(ex), 500  # Return an error message if something goes wrong



def read_from_file(file_path):
    with open(file_path, 'r') as file:
        return file.read().strip()

def openAi_correct_text(text, lesson_requirement):
    relative_path_api_key = '../../openAi_key.txt'  # Exemplu de cale relativă
    api_key_path = Path(relative_path_api_key).resolve()
    api_key = read_from_file(api_key_path)

    relative_path_organization_id = '../../openAi_organization-id.txt'  # Exemplu de cale relativă
    organization_id_path = Path(relative_path_organization_id).resolve()
    organization_id = read_from_file(organization_id_path)

    relative_path_project_id = '../../openAi_project_id.txt'  # Exemplu de cale relativă
    project_id_path = Path(relative_path_project_id).resolve()
    project_id = read_from_file(project_id_path)

    client = OpenAI(
        api_key = api_key,
        organization = organization_id,
        project = project_id,
    )

    stream = client.chat.completions.create(
        model="gpt-3.5-turbo",
        messages=[{
            "role": "user", 
            "content": "Please correct the spelling errors in the following text without changing the existing words or adding new ones. The essay must meet the lesson requirement: {}. Here is the text: {}. Please provide only the corrected text without any additional explanation. For example changing 'ky' to 'my' and 'namq' to 'name', etc.".format(lesson_requirement, text)
            }],
        stream=True,
    )

    corrected_text = ""

    for chunk in stream:
        if chunk.choices[0].delta.content is not None:
            corrected_text += chunk.choices[0].delta.content

    stream = client.chat.completions.create(
        model="gpt-3.5-turbo",
        messages=[{
            "role": "user", 
            "content": "Please evaluate the following text and return a score from 1 to 100, along with an explanation of the score, based on how well it meets the lesson requirement: {}. Here is the text: {}. The response should follow this pattern: 'score|score explanation'.".format(lesson_requirement, corrected_text)
            }],
        stream=True,
    )

    score_answer = ""

    for chunk in stream:
        if chunk.choices[0].delta.content is not None:
            score_answer += chunk.choices[0].delta.content

    final_score, score_explanation = score_answer.split('|', 1)

    # print('\n\n\t\tFINAL SCORE: ', final_score, '\n\n\t\tSCORE EXPLANATION: ', score_explanation)

    return corrected_text, final_score, score_explanation

def detect_text_lines(image):
    gray_image = cv.cvtColor(image, cv.COLOR_BGR2GRAY)
    _, binary_image = cv.threshold(gray_image, 127, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
   
    upper = []
    lower = []
    flag = True
    for i in range(binary_image.shape[0]):
        column = binary_image[i:i+1,:]
        pixel_count = np.count_nonzero(column == 255)
        if flag:
            if pixel_count >0:
                upper.append(i)
                flag=False
        else:
            if pixel_count < 7:
                lower.append(i)
                flag=True
    text_lines = []

    if len(upper)!= len(lower):
        lower.append(binary_image.shape[0])

    # print("\n\nupper: ", upper)
    # print("\n\nlower: ", lower)

    for i in range(len(upper)):
        line_image = image[upper[i]:lower[i],0:]
        
        if line_image.shape[0]>5:
            # plt.imshow(line_image)
            # plt.show()
            line_image=cv.resize(line_image,((line_image.shape[1]*5,line_image.shape[0]*8)))
            text_lines.append(line_image)

    return text_lines

def detect_words(text_lines, max_word_spacing=30):
    word_images = []
    word_counter=0
    line_counter=0

    try:
        for line_image in text_lines:
            gray_image = cv.cvtColor(line_image, cv.COLOR_BGR2GRAY)
            _, threshed = cv.threshold(gray_image, 100, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
            final_thr = cv.dilate(threshed,None,iterations = 20)

            # plt.imshow(final_thr)
            # plt.show()
            
            contours, _ = cv.findContours(final_thr,cv.RETR_EXTERNAL,cv.CHAIN_APPROX_SIMPLE)
            boundingBoxes = [cv.boundingRect(c) for c in contours]
            (contours, boundingBoxes) = zip(*sorted(zip(contours, boundingBoxes), key=lambda b: b[1][0], reverse=False))

            prev_x, prev_w, prev_contour = None, None, None
            new_contours = []
            for cnt in contours:
                area = cv.contourArea(cnt)
                x, y, w, h = cv.boundingRect(cnt)
                if area > 10000:
                    if prev_x is not None:
                        # calculeaza distanta dintre contururile consecutive
                        spacing = x - (prev_x + prev_w)
                        if spacing < max_word_spacing:
                            # extrage coordonatele conturului anterior
                            prev_contour = new_contours.pop()
                            prev_x, prev_y, prev_w, prev_h = cv.boundingRect(prev_contour)
                            # actualizeaza coordonatele si dimensiunile conturului curent
                            x = min(x, prev_x)
                            y = max(y, prev_y)
                            w = w + prev_w
                            h = max(h, prev_h)
                            # cream un nou contur folosind coordonatele actualizate
                            new_contour = np.array([[[x, y]], [[x + w, y]], [[x + w, y + h]], [[x, y + h]]])
                            # adaugam noul contur concatenat în lista new_contours
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
                    letterBgr = line_image[0:line_image.shape[1], x:x + w]
                    word_images.append(letterBgr)

                    # plt.imshow(letterBgr)
                    # plt.show()

                    cv.imwrite("./result/words/" + str(word_counter) + ".jpg", letterBgr)
                    word_counter += 1
            line_counter += 1
        
        return word_images
    except Exception as e:
            print ('Error Message ',e)

def calc_histogram(image, width):
    cropped_image = image[:, 0:width]
    # plt.imshow(cropped)
    # plt.show()
    column_count = np.sum(cropped_image==255, axis=0)
    x_values = list(range(len(column_count)))
    plt.plot(column_count)
    plt.fill_between(x_values, column_count, 1, facecolor='blue', alpha=0.5)
    # plt.show()  
    return column_count     

def detect_characters_lines(copy_letter, min_pixel_threshold, min_separation_threshold, min_round_letter_threshold, colcnt, letterGray, h):  
    # seg = []
    segment_points_1 = []
    segment_points_2 = []
    ## Check if pixel count is less than min_pixel_threshold, add segmentation point
    for i in range(len(colcnt)):
      if(colcnt[i] > min_pixel_threshold):
         segment_points_1.append(i)
          
    # verifica daca distanta dintre doua pct consecutive este mai mare decat min_separation_threshold
    for i in range(len(segment_points_1)-1):
        if segment_points_1[i+1] - segment_points_1[i] > 1:
            if(segment_points_1[i+1] - segment_points_1[i] > min_separation_threshold):
                segment_points_2.append(segment_points_1[i])
 
    # adaugam un mic offset pentru a crea o mica separare intre litere, sa nu fie prea apropiate
    for i in range(len(segment_points_2)):
        segment_points_2[i] = segment_points_2[i] + 8

    if len(segment_points_2) == 0:
        letter3 = copy_letter
    else:
        letter3 = copy_letter.copy()
        for i in range(len(segment_points_2)):
            letter3 = cv.line(letter3, (segment_points_2[i], 0), (segment_points_2[i], h), (255, 0, 0), 2)
    
    # plt.imshow(letter3)
    # plt.show()
    return segment_points_2 

def characters_segmentation(seg,lettergray):
    s=0
    wordImgList = []
    fn = 0

    for i in range(len(seg)):
        if i==0:
            s=seg[i]
            if s > 15:
                wordImg = lettergray[0:,0:s]
                cntx=np.count_nonzero(wordImg == 255) 
                # plt.imshow(wordImg)
                # plt.show()
                fn=fn+1
            else:
                continue
        elif (i != (len(seg))):
            if seg[i]-s > 15:
                wordImg = lettergray[0:,s:seg[i]]
                cntx=np.count_nonzero(wordImg == 255) 
                # plt.imshow(wordImg)
                # plt.show()
                fn=fn+1
                s=seg[i]
            else:
                continue
        wordImg = np.pad(wordImg, ((0, 0), (175, 175)), mode='constant', constant_values=0)
        wordImgList.append(wordImg)

    if len(seg) > 0:
        wordImg = lettergray[0:,seg[len(seg)-1]:]
    else:
        wordImg = lettergray[0:,0:]

    cntx=np.count_nonzero(wordImg == 255) 
    # plt.imshow(wordImg)
    # plt.show()
    fn=fn+1
    wordImg = np.pad(wordImg, ((0, 0), (175, 175)), mode='constant', constant_values=0)
    wordImgList.append(wordImg)


    return wordImgList

def clean_directory(directory):
    try:
        for root, dirs, files in os.walk(directory, topdown=False):
            for name in files:
                os.remove(os.path.join(root, name))
            for name in dirs:
                os.rmdir(os.path.join(root, name))
        # for foldername in os.listdir(directory):
        #         folder_path = os.path.join(directory, foldername)
        #         # Remove all files in the folder
        #         for filename in os.listdir(folder_path):
        #             file_path = os.path.join(folder_path, filename)
        #             os.remove(file_path)
        #         # Remove the folder itself
        #         os.rmdir(folder_path)
    except Exception as e:
        print ('Error Message ',e)
        pass

if __name__ == '__main__':
    app.run(host="0.0.0.0", port=5100)







def detect_text_lines(image):
    gray_image = cv.cvtColor(image, cv.COLOR_BGR2GRAY)
    _, binary_image = cv.threshold(gray_image, 127, 255, cv.THRESH_BINARY_INV|cv.THRESH_OTSU)
   
    flag = True
    for i in range(binary_image.shape[0]):
        column = binary_image[i:i+1,:]
        pixel_count = np.count_nonzero(column == 255)
        if flag:    # daca suntem la inceputul unei linii
            if pixel_count >0:
                upper.append(i)
                flag=False
        else:   # daca suntem in interiorul unei linii (nr mix de pixeli)
            if pixel_count < 7:
                lower.append(i)
                flag=True
    text_lines = []

    if len(upper)!= len(lower):
        lower.append(binary_image.shape[0])

    for i in range(len(upper)):
        line_image = image[upper[i]:lower[i],0:]    # extrage imaginea liniei
        if line_image.shape[0]>5:
            line_image=cv.resize(line_image,((line_image.shape[1]*5,line_image.shape[0]*8)))
            text_lines.append(line_image)

    return text_lines