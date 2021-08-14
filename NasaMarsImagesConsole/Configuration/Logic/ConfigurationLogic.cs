using Newtonsoft.Json;
using System;
using System.IO;

namespace NasaMarsImagesConsole
{
    public class ConfigurationLogic
    {
        private Configuration _config;
        //Read the Console Configuration file and deserialize to required object type.
        public Configuration GetConfiguration(string configPath)
        {
            var json = File.ReadAllText(configPath);
            _config = JsonConvert.DeserializeObject<Configuration>(json);

            IsPathValid(_config.InputDatesFilePath, "Input Dates File", false);
            IsPathValid(_config.OutputImageDirectoryPath, "Output Image File", true);

            return _config;
        }

        /// Check if the Directory/File path is Valid, else return error message
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="checkforDir">Pass true if you're validating Directory path, default is false</param>
        /// <param name="name">Name of the path</param>
        private void IsPathValid(string path, string name, bool checkforDir = false)
        {
            if (checkforDir)
            {
                if (!Directory.Exists(path))
                {
                    throw new FormatException(name + "direcotry path is not valid");
                }
            }
            else
            {
                if(!File.Exists(path))
                {

                    throw new FormatException(name + "file path is not valid");
                }
            }


        }
    }
}
