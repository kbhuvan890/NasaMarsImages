using Microsoft.VisualStudio.TestTools.UnitTesting;
using NasaMarsImageServices;
using NasaMarsImageServices.BusinessLogic;

namespace NasaMarsImageUnitTest
{
    [TestClass]
    public class NasaMarsImageService_UnitTesting
    {
        private readonly MarsImageLogic _marsService;
        private readonly MarsImageServiceConfiguration _config;

        public NasaMarsImageService_UnitTesting()
        {
            _config = new MarsImageServiceConfiguration {
                AccessKey = "NC2AZKxmFDppkoHOy3dKg3Yzd8NHsFtT5RzuO7wc", 
                BaseUri = "https://api.nasa.gov/", 
                ImageEndpoint= "mars-photos/api/v1/rovers/curiosity/photos" };

            _marsService = new MarsImageLogic(_config);
        }

        [TestMethod]
        public void IsDateInValid_ReturnTrue()
        {
            var imgLog = _marsService.GetPhotos("2021-04-31", "C:\\Mars\\UnitTest\\");

            Assert.IsTrue(imgLog.ApiReturnedImageCount == new ImageLog().ApiReturnedImageCount, "Input Date is invalid");
        }

        [TestMethod]
        public void IsDateIsValid_ReturnFalse()
        {
            var imgLog = _marsService.GetPhotos("2021-05-31", "C:\\Mars\\UnitTest\\");

            Assert.IsFalse((imgLog == new ImageLog()), "Input Date is Valid");
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
        public void IsOutputDirectoryInValid_ReturnTrue()
        {
            _marsService.GetPhotos("2021-04-30", "J:\\Mars\\UnitTest\\");
        }

        [TestMethod]
        public void IsOutputDirectoryIsValid_ReturnFalse()
        {
            var imgLog = _marsService.GetPhotos("2021-05-31", "C:\\Mars\\UnitTest\\");

            Assert.IsFalse(imgLog == new ImageLog(), "Output Directory is Valid");
        }
    }
}
