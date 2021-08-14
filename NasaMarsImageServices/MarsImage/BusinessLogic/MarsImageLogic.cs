using NasaMarsImageServices.Models;
using NasaMarsImageServices.ServiceLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NasaMarsImageServices.BusinessLogic
{
    public class MarsImageLogic
    {

        #region Variables
        private readonly MarsImageServiceConfiguration _config;
        private readonly ApiServiceLogic _apiLogic;
        #endregion

        public MarsImageLogic(MarsImageServiceConfiguration config)
        {
            _config = config;
            _apiLogic = new ApiServiceLogic(_config.BaseUri);
        }


        /// <summary>
        /// To fetch the Mars photos from NASA
        /// </summary
        /// <param name="date"></param>
        /// <param name="outputDirectoryPath">Directory path to store the image</param>
        /// <returns></returns>
        public ImageLog GetPhotos(string date, string outputDirectoryPath)
        {
            //Valid the inputs
            var requestDate = GetParsedDate(date);
            var storagePath = GetValidOutputDirectoryPath(requestDate, outputDirectoryPath);

            //Initialize return object
            var imglog = new ImageLog();

            //If request date is empty, return initized object.
            if (string.IsNullOrEmpty(requestDate))
            {
                return imglog;
            }            

            //Else continue
            //Initialize query parameters
            var queryParams = new Dictionary<string, string>()
            {
               {"earth_date", requestDate },
               {"api_key", _config.AccessKey }
            };

            //Call the API Service
            var lPhotos = _apiLogic.CallService<PhotoList>(_config.ImageEndpoint, queryParams);
            imglog.ApiReturnedImageCount = lPhotos.photos.Count;            
            imglog.ImageStoredCount =  StoreImages(lPhotos, storagePath);

            return imglog;
        }

        /// <summary>
        /// Prase the input date
        /// </summary>
        /// <param name="strDate">input date</param>
        /// <returns></returns>
        private string GetParsedDate(string strDate)
        {
            if (DateTime.TryParse(strDate, out DateTime dt))
            {
                return dt.ToString("yyyy-MM-dd");
            }
            return string.Empty;
        }

        /// <summary>
        /// Create new folder by date within the output Directory Path given as input
        /// </summary>
        /// <param name="date">request date</param>
        /// <param name="outputDirectoryPath">Direcotry to store the images</param>
        /// <returns></returns>
        private string GetValidOutputDirectoryPath(string date, string outputDirectoryPath)
        {
            var path = outputDirectoryPath + date + "\\";

            //Check if the output file path is valid
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                    return path;
                }
                catch (Exception)
                {
                    throw;
                }             
            }
            else
            {
                return path;
            }

        }

        /// <summary>
        /// Store the Images returned by the API result
        /// </summary>
        /// <param name="date">Date for which the images was requested</param>
        /// <param name="photoList">Result from the API</param>
        /// <param name="outputDirectoryPath">Directory to store the photos</param>
        /// <returns></returns>
        private int StoreImages(PhotoList photoList, string outputDirectoryPath)
        {
            return DownloadLogic.DownloadFileAsync(photoList.photos.Select(x => x.img_src).ToList(), outputDirectoryPath).Result;
        }

    }
}
