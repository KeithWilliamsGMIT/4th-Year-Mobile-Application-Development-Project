# Mobile Application Development Project
This project is for my 4th year Mobile Application Development module in college. This README outlines some of the high level detail of the project. More information is available on the project [wiki](https://github.com/KeithWilliamsGMIT/4th-Year-Mobile-Application-Development-Project/wiki).

## Requirements
The requirements of this project is to develop a mobile application that runs on the Universal Windows Platform. This mobile application must also interact with some web service hosted on Azure. This web service can be written in any language. The mobile application must be **submitted** for certification to the Windows Store by the given deadline.

## Objectives
The objectives I've outlined for this project are listed below.
* Create a cross-platform app using the Xamarin framework.
* Use an additional external service.
* Use at least one device sensor.
* Utilise the GitHub Flow workflow for managing the development of this project.
* Develop the application in an iterative and incremental manner.
* Submit the finished application to Windows Store and Play Store.

## Project proposal
The idea proposed for this project is to develop a mobile application to keep a digital and searchable archive of receipts. In order to help manage the scope of this project, the plan is broken into two distinct phases. The first phase focuses on delivering a minimal viable product (MVP). The second phase involves adding possible enhancements.

### MVP
The user will login to their account via an external social service. From here there will be two options. They can:
1. Take a picture of a new receipt.  
When the user takes a picture of a receipt, the image will be sent to the web service on Azure and saved to a path. However, the date on the receipt will also be extracted from that image (if possible) using some OCR (Optical character recognition) library. This date, the path to the original image and a timestamp will then be stored in a database.
2. Search for old receipts by date.  
The date extracted from the image will be used to make the images searchable. When a receipt, or receipts, are returned following a search query there will be an option to view the original image.

The purpose of this project is to help users track and manage expenses.

### Enhancements
The main enhancement for this project is orientated around also extracting the total expense, and possibly the expense of each individual item, from the receipt. This information can then be used to provide a visual representation of spending over time. However, the problems with implementing this feature are as follows:
1. The text extraction must be more accurate.
2. Currency may be harder to identify in a string of text.
3. Implementing this functionality will be time-consuming.

## Getting Started
This section will outline the steps involved in setting up the project on your local machine for development purposes. The first step is to clone the repository.

```
git clone https://github.com/KeithWilliamsGMIT/4th-Year-Mobile-Application-Development-Project.git
cd 4th-Year-Mobile-Application-Development-Project
```

### Web Service
First ensure you have Python 3 installed. The easiest way to install Python is through [Anaconda](https://www.anaconda.com/downloads). Next create a virtual environment in the web-service folder to store all the required packages. Once created, activate the new environment using the `source` command and install the packages listed in the `requirements.txt` file.

```
cd web-service

# Create the virtual environment
virtualenv venv

# Activate the environment
venv/Scripts/activate

# Install the requirements
pip install -r requirements.txt
```

Before starting the web service you will also need to download the Tesseract OCR library on which it depends. The [instructions](https://github.com/tesseract-ocr/tesseract/wiki) are available on the project wiki on GitHub. A local instance of MongoDB must also be running. Instructions for installing MongoDB can be found on the online [manual](https://docs.mongodb.com/manual/installation/). Finally, use the below commands to start the web service.

```
python3 main.py
```

The web service should now be running on [http://localhost:5000](http://localhost:5000).

### Project Outcomes
The following is a list of project outcomes.
* Used the MVVM design pattern to develop the application.
* Developed a cross-platform application for UWP and Android using Xamarin.
* 3-tier architecture
  * Mobile application (Presentation layer)
  * Python web service (Application layer)
  * MongoDB (Data layer)
* Application has full CRUD functionality.
  * Create, Read, Update, Delete
* Used the device camera for taking pictures of receipts.
* Application was built with localisation in mind (Strings put in resource file).
* User can login using different identity providers (Google and Microsoft)
* Developed the interface with user experience in mind:
  * Simple UI that works on both mobile and desktop.
  * Minimised number of controls on screen.
  * Tried to minimise clicks needed to navigate around application.
  * Used icons instead of text where appropriate.
  * Added messages to inform the user of whats happening.
  * Consistant colour theme throughout application.
* Developed the application in an Agile manner:
  * Used the GitHub Flow
  * Used GitHub Issues
  * Used GitHub Projects
  * Used GitHub Wiki
  * Develop the application in an iterative and incremental manner.
* Deployed the web service to Azure and the MongoDB is hosted on MLab.
* Submit the finished application to Windows Store and Play Store.

### Enhancements
* Extract data from image of receipt  
This was one of the original objectives outlined at the start of the project. The goal was to implement some way to parse data from an image of a receipt. The goal was to improve the user experience by saving the user time as they would not need to enter the date and total amount due when saving a new receipt to the database. First I attempted to implement this on the server side as I thought it would be an intensive task for mobile devices. However, although I was able to get the basic functionality working locally, it used the Tesseract library, which I was unable to install on Azure. Next, I tried to implement it client side but could not find a cross-platform solution that would work on both UWP and Android.One solution to this might be to use Microsoft Cognitive Services to extract the text from an image. However, I had already invested too much time into this feature and could not afford to invest any more. Therefore, I've left this feature as a possible enhancement.
* Offline use.  
Currently, the application requires an internet connection. This is because of it's heavy reliance on the web service. Also, due to the nature of the data this application handles, the user must sign in to the application to view receipts. One possible enhancement that could be added would be to allow the user to add a receipt when offline, which would be sent to saved to the database when the user connects to the internet. This would involve allowing the user access to the add receipt page when offline, save the json document to local storage and then when the device is connected to the internet start a background task that would save the receipts to the database. However, using local store, starting background tasks and checking the internet connection can be difficult when developing a cross-platform application. Therefore, I decided not to use this approach. Instead, to overcome this, I added an option to the add receipt page to browse for an image. Therefore, the user can take a receipt of an image at anytime and then save use that the next time they are connected to the internet. Another possible solution would be to implement PouchDB on the client and CouchDB on the server and have them sync automatically.
* Client-managed authentication  
Currently a server-managed authentication flow is used to authenticate users. While this is the easier and quicker of the two to implement, the client-managed authentication flow would offer a better user experience and should be considered as a possible enhancement.
* Analyse data
This feature was always out of scope for this project, but the idea is to help the user track and manage their finances. A feature where the user can monitor and analyse the data through the use of charts and graphs would be a very useful future enhancement.

### Conclusion
After completing this project I have found that there is still significantly more work involved in developing a cross-platform application. Due to differences between platforms I was unable to implement some useful functionality that would improve the user experience. However, I have found workarounds for each of these situations. Perhaps the scope of this project was too big in relative to the timeframe. Therefore, if I were to do this project again I would only develop for UWP. Xamarin is useful for simple apps but when dealing with even moderately complex applications the project can get quite difficult to manage quite quick. The plugins available for Xamarin solve this problem to an extent, but not entirely. While there is still additional features that can be added to this project, I think that it was a reasonable first attempt at developing a cross-platform application.