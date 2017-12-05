# Author:		Keith Williams
# Date:			04/10/2017
# Description:	Define the API for the web service.

from flask import Flask, request
from json import dumps
import os

from .database import retrieve_user_receipts, create_user_receipt, delete_user_receipt

app = Flask(__name__)

# GET - Return users receipts created after a given timestamp.
@app.route('/api/<user_id>/receipts/<issued_on>', methods=['GET'])
def get_receipts(user_id, issued_on):
	response = {'status': 'success', 'message': 'Successfully retrieved receipts for user - ' + user_id, 'receipts': None}
	
	response['receipts'] = retrieve_user_receipts(user_id, issued_on)
	
	return dumps(response)

# POST - Add the given receipt to the users list of receipts.
@app.route('/api/<user_id>/receipt', methods=['POST'])
def post_receipt(user_id):
	response = {'status': 'success', 'message': 'Successfully added receipt for user - ' + user_id}
	
	data = request.get_json()
	
	create_user_receipt(user_id, data)
	
	return dumps(response)

# DELETE - Delete the given receipt from the users list of receipts.
@app.route('/api/<user_id>/receipt/<receipt_id>', methods=['DELETE'])
def delete_receipt(user_id, receipt_id):
	response = {'status': 'success', 'message': 'Successfully delete receipt for user - ' + user_id}
	
	delete_user_receipt(user_id, receipt_id)
	
	return dumps(response)