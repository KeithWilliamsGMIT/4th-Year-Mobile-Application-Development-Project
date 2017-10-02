# Mobile Application Development Project
This project is for my 4th year Mobile Application Development module in college. The requirements of this project is to develop a mobile application that runs on the Universal Windows Platform. This mobile application must also interact with some web service hosted on Azure. This web service can be written in any language. The mobile application must be **submitted** for certification to the Windows Store by the given deadline.

## Objectives
The objectives I've outlined for this project are listed below.
* Create a cross platform app using the Xamarin framework.
* Use an additional external service.
* Use at least one device sensor.
* Utitise the GitHub Flow workflow for managing the development of this project.
* Develop the application in an iterative and incrementive manner.
* Submit the finished application to Windows Store and Play Store.

## Project proposal
The idea proposed for this project is to develop a mobile application to keep a digital and searchable archive of receipts. The user will login to their account via an external social service. From here there will be two options. They can:
1. Take a picture of a new receipt.  
When the user takes a picture of a receipt, the image will be sent to the web service on Azure and saved to a path. However, the date on the receipt will also be extracted from that image (if possible) using some OCR (Optical character recognition) library. This date, the path to the original image and a timestamp will then be stored in a database.
2. Search for old receipts by date.  
The date extracted from the image will be used to make the images searchable. When a receipt, or receipts, are returned following a search query there will be an option to view the original image.

The purpose of this project is to help users track and manage expenses.