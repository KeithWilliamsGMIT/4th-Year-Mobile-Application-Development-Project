# Author:		Keith Williams
# Date:			06/10/2017
# Description:	This module will be used to connect to the database used for this application.

import pymongo
import os

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

# Return a receipt document with a matching receipt_id.
# If no receipt matching the query is found return None.
def retrieve_receipt(receipt_id):
	receipt = receipt_collection.find_one({'receipt_id': receipt_id})
	
	return receipt

# Create a new receipt document in MongoDB.
def create_receipt(data):
	receipt_collection.insert_one(data)