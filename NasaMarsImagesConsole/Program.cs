using NasaMarsImageServices.BusinessLogic;
using System;
using System.IO;

namespace NasaMarsImagesConsole
{
    public class Program
    {
        private static Configuration _config;

        static void Main()
        {
            try
            {
                ReadConfig();
                DownloadImagesbyDate();
                WriteToLog("------------End of Program------------");
            }
            catch (Exception ex)
            {
                WriteToLog(ex.Message + "; " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Read the configutation file
        /// </summary>
        private static void ReadConfig()
        {
            var configfilepath = Path.Combine(Directory.GetCurrentDirectory(), "Configuration.json");
            _config = new ConfigurationLogic().GetConfiguration(configfilepath);
            WriteToLog("Reading Config Completed");
        }

       /// <summary>
       /// Download Images by dates found in the input file
       /// </summary>
        private static void DownloadImagesbyDate()
        {
            string[] datesArray = File.ReadAllLines(_config.InputDatesFilePath);

            var marsImgLogic = new MarsImageLogic(_config.MarsImageAPIConfig);

            foreach (var strDate in datesArray)
            {
                if (DateTime.TryParse(strDate, out DateTime dt))
                {
                    var imglog = marsImgLogic.GetPhotos(dt.ToString("yyyy-MM-dd"), _config.OutputImageDirectoryPath);
                    WriteToLog("Date : " + strDate + " | Api Image Count : " + imglog.ApiReturnedImageCount + " | Image Stored Count : " + imglog.ImageStoredCount + " | Match : " + (imglog.ApiReturnedImageCount == imglog.ImageStoredCount).ToString());
                }
                else
                {
                    WriteToLog("Date : " + strDate + " | Image Count : 0 | Exception : Input Date is not a valid date !");
                }
            }
        }

        /// <summary>
        /// Write data to log
        /// </summary>
        /// <param name="log">data to be written</param>
        private static void WriteToLog(string log)
        {
            var logdata = DateTime.Now + " " + log + Environment.NewLine;
            File.AppendAllText(_config.LogDirectoryPath + "Log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", logdata);
            Console.WriteLine(logdata);
        }

    }
}
