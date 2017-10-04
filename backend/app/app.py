# Author:		Keith Williams
# Date:			02/09/2017
# Description:	Define the API for the web service.

from flask import Flask, request
import os

app = Flask(__name__)

# POST - Return all the users receipts.
@app.route('/api/receipts', methods=['GET'])
def receipts():
	return '[GET] receipts'

# GET - Return a requested receipt if it exists.
# POST - Add the given receipt to the users list of receipts.
@app.route('/api/receipt', methods=['GET', 'POST'])
def receipt():
	if request.method == 'GET':
		return '[GET] receipt'
	else:
		return '[POST] receipt'

# Only run if this is the main module.
if __name__ == '__main__':
	app.debug = True
	
	# The secret key is used to generate access tokens
	app.secret_key = os.urandom(24)
	
	# Set the host to 0.0.0.0 to make it accessible to other
	app.run(host="0.0.0.0", threaded=True)