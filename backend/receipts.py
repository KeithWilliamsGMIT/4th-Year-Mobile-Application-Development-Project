# Author:		Keith Williams
# Date:			02/09/2017
# Description:	This module will be used to extract data from images of receipts.

import sys
from PIL import Image
from pytesseract import image_to_string

# Extract all text from an image.
# Return the extracted text.
def extract_text_from_image(filename):
	# Open the image
	image = Image.open(filename)
	
	# Extract all text
	text = image_to_string(image, lang='eng', config='-psm 4') # Read as single column
	
	return text

# Run only if this is the main module.
if __name__ == '__main__':
	try:
		filename = sys.argv[1]
		text = extract_text_from_image(filename)
		print(text)
	except ValueError:
		print('An error occurred! Please ensure that you passed in a valid path to an image file.')