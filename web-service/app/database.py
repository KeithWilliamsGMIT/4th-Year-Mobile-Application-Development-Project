# Author:		Keith Williams
# Date:			06/10/2017
# Description:	This module will be used to connect to the database used for this application.

import pymongo
import uuid
import os

from bson.json_util import dumps

# Specify connection information for MongoDB
host = os.environ.get('MONGO_HOST', 'localhost')
port = int(os.environ.get('MONGO_PORT', 5000))
user = os.environ.get('MONGO_USER', 'root')
password = os.environ.get('MONGO_PASSWORD', 'password')

# Connect to Mongo
mongo = pymongo.MongoClient(host=host, port=port)

# Get the Mongo database
mongodb = mongo['digireceipt']

# Authenticate user on Mongo database
mongodb.authenticate(user, password)

# Get the collection of receipts
receipt_collection = mongodb['receipt-collection']

# Return all receipt document belonging to the given user.
# If no receipt matching the query is found return None.
def retrieve_user_receipts(user_id, issued_on):
	receipts = receipt_collection.find({'$and': [{'userId': user_id}, {'issuedOn': {'$lt': issued_on}}] }, {'_id': False}).sort('issuedOn', -1).limit(5)
	
	return dumps(receipts)

# Create a new receipt document in MongoDB.
def create_user_receipt(user, data):
	data['userId'] = user
	data['receiptId'] = uuid.uuid4().hex
	
	receipt_collection.insert_one(data)

# Delete a receipt document from MongoDB.
def delete_user_receipt(user_id, receipt_id):
	receipts = receipt_collection.find({'$and': [{'userId': user_id}, {'receiptId': receipt_id}] })