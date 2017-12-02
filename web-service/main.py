from flask import Flask
from app.views import app

# Only run if this is the main module.
if __name__ == '__main__':
	app.debug = True
	
	# Start the web service.
	app.run(threaded=True)