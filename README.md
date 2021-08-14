# NasaMarsImages
Project to Access NASA API and download the images by date.

The NaraMarsImages Solution Contains three projects 
**#NasaMarsImagesConsole**
**#NasaMarsImageServices**
**#NasaMarsImageUnitTesting**

# NasaMarsImagesConsole

 The console project will read the configuration file  and download and store the images from the API . Also logs the information returned from the application.
 
 **Configuration file**  - you need to set this before running the console job
 
 ```json
  {
  "LogDirectoryPath": "C:\\Mars\\Logs\\",
  "InputDatesFilePath": "C:\\Mars\\Input\\dates.txt",
  "OutputImageDirectoryPath": "C:\\Mars\\MarsOutputImage\\",
  "MarsImageAPIConfig": {
    "AccessKey": "NC2AZKxmFDppkoHOy3dKg3Yzd8NHsFtT5RzuO7wc",
    "BaseUri": "https://api.nasa.gov/",
    "ImageEndpoint": "mars-photos/api/v1/rovers/curiosity/photos"
	
	}
} 
```

# NasaMarsImageServices

The service library contains the  business logic to access the NASA API and  to download the images.

# NasaMarsImageUnitTest

This project contains the unit test features for the  NasaMarsImagesServices

