# Author:		Keith Williams
# Date:			04/10/2017
# Description:	Define the API for the web service.

from flask import Flask, request
from json import dumps
import os

from .database import retrieve_user_receipts, create_receipt

app = Flask(__name__)

# GET - Return all the users receipts.
@app.route('/api/<user>/receipts', methods=['GET'])
def get_receipts(user):
	response = {'status': 'success', 'message': 'Successfully retrieved receipts for user - ' + user, 'receipts': None}
	
	response['receipts'] = retrieve_user_receipts(user)
	
	return dumps(response)

# POST - Add the given receipt to the users list of receipts.
@app.route('/api/<user>/receipt', methods=['POST'])
def post_receipt(user):
	response = {'status': 'success', 'message': 'Successfully added receipt for user - ' + user}
	
	data = request.get_json()
	data['user_id'] = user
	
	create_receipt(data)
	
	return dumps(response)