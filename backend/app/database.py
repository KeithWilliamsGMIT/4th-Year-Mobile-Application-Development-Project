# Author:		Keith Williams
# Date:			06/10/2017
# Description:	This module will be used to connect to the database used for this application.

import pymongo

# Specify connection information for MongoDB
MONGO_HOST='localhost'
MONGO_PORT=27017

# Connect to Mongo
mongo = pymongo.MongoClient(
	MONGO_HOST,
	MONGO_PORT)

# Get the Mongo database
mongodb = mongo['receipt-database']

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