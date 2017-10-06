# Author:		Keith Williams
# Date:			04/10/2017
# Description:	Define the API for the web service.

from flask import Flask, request
import os

app = Flask(__name__)

# POST - Return all the users receipts.
@app.route('/api/<user>/receipts', methods=['GET'])
def receipts(user):
	return '[GET] receipts for ' + user

# GET - Return a requested receipt if it exists.
# POST - Add the given receipt to the users list of receipts.
@app.route('/api/<user>/receipt', methods=['GET', 'POST'])
def receipt(user):
	if request.method == 'GET':
		return '[GET] receipt for ' + user
	else:
		return '[POST] receipt for ' + user

# Only run if this is the main module.
if __name__ == '__main__':
	app.debug = True
	
	# The secret key is used to generate access tokens
	app.secret_key = os.urandom(24)
	
	# Set the host to 0.0.0.0 to make it accessible to other
	app.run(host="0.0.0.0", threaded=True)