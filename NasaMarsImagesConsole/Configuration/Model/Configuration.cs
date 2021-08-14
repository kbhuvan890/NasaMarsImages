using NasaMarsImageServices;

namespace NasaMarsImagesConsole
{
    public class Configuration
    {
        public string LogDirectoryPath { get; set; }

        public string InputDatesFilePath { get; set; }

        public string OutputImageDirectoryPath { get; set; }

        public MarsImageServiceConfiguration MarsImageAPIConfig { get; set; }

    }
}
