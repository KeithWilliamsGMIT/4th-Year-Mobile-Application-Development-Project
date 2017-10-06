# Author:		Keith Williams
# Date:			02/10/2017
# Description:	This module will be used to extract data from images of receipts.

import re
import sys
from PIL import Image
from pytesseract import image_to_string

# Extract all text from an image.
# Return the extracted text.
def extract_text_from_image(filename):
	# Open the image
	image = Image.open(filename)
	
	# Extract all text (Read as single column)
	text = image_to_string(image, lang='eng', config='-psm 4')
	
	return text

# Parse the first date from a string of text.
# Return the date as a string if it exists.
# Otherwise return None.
def parse_date(text):
	# List of posible delimiters used in dates
	delimiters = ['/', '-', '.']
	
	# Parse the text and search for a string that represents a date
	for delimiter in delimiters:
		delimiter = re.escape(delimiter)
		date = re.search(r'(\d+' + delimiter + '\d+' + delimiter + '\d+)', text)
		
		if date is not None:
			return date.group(0)
	
	return None

# Run only if this is the main module.
if __name__ == '__main__':
	try:
		filename = sys.argv[1]
		text = extract_text_from_image(filename)
		date = parse_date(text)
		print('Date extracted from image: ' + date)
	except ValueError:
		print('An error occurred! Please ensure that you passed in a valid path to an image file.')