# Author:		Keith Williams
# Date:			04/10/2017
# Description:	Define the API for the web service.

from flask import Flask, request
import os

from .database import retrieve_receipt, create_receipt

app = Flask(__name__)

# GET - Return all the users receipts.
@app.route('/api/<user>/receipts', methods=['GET'])
def get_receipts(user):
	return '[GET] receipts for ' + user

# GET - Return a requested receipt if it exists.
@app.route('/api/<user>/receipt/<receipt>', methods=['GET'])
def get_receipt(user, receipt):
	message = retrieve_receipt(receipt)
	
	if message is None:
		message = 'No receipt with id ' + receipt + ' found!'
	
	return '[GET] receipt for ' + user + ' - ' + message

# POST - Add the given receipt to the users list of receipts.
@app.route('/api/<user>/receipt', methods=['POST'])
def post_receipt(user):
	return '[POST] receipt for ' + user