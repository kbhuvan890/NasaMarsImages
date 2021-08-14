namespace NasaMarsImageServices
{
    public class ImageLog
    {
        public int ApiReturnedImageCount { get; set; }

        public int ImageStoredCount { get; set; }

        public ImageLog()
        {
            ApiReturnedImageCount = 0;
            ImageStoredCount = 0;
        }
    }
}
