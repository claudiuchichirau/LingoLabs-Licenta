import cv2
import numpy as np
import base64
import io

def process_image(image_base64):
    # Decode the Base64 string back into a byte array
    image_bytes = base64.b64decode(image_base64)

    # Convert the byte array to a numpy array
    image_array = np.fromstring(image_bytes, dtype=np.uint8)

    # Decode the image from the array
    img = cv2.imdecode(image_array, cv2.IMREAD_GRAYSCALE)

    # Aplica un filtru Gaussian pentru a reduce zgomotul
    img = cv2.GaussianBlur(img, (5, 5), 0)

    # Binarizeaza imaginea
    _, img = cv2.threshold(img, 127, 255, cv2.THRESH_BINARY_INV)

    # Gaseste contururile
    contours, _ = cv2.findContours(img, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    characters = []
    for contour in contours:
        # Obtine dreptunghiul de incadrare pentru fiecare contur
        x, y, w, h = cv2.boundingRect(contour)

        # Extrage caracterul
        character = img[y:y+h, x:x+w]

        # Redimensioneaza caracterul la dimensiunea dorita
        character = cv2.resize(character, (20, 20))

        characters.append(character)

    return characters