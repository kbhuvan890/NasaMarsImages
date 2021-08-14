using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace NasaMarsImageServices
{
    public static class DownloadLogic
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// (Synchronously) Download image from given URI and save in output path
        /// </summary>
        /// <param name="uri">Image URI</param>
        /// <param name="outputDirectoryPath">Directory path to store the image</param>
        public static void DownloadFile(string uri, string outputDirectoryPath, string imageName)
        {
            //Check of the Image URI is valid
            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult))
                throw new InvalidOperationException("Image URI is invalid.");

            //Fetch the Image in bytes[]
            byte[] fileBytes = client.GetByteArrayAsync(uriResult).Result;

            //Write the image to a file
            File.WriteAllBytes(outputDirectoryPath + imageName, fileBytes);
        }

        /// <summary>
        /// (Asynchronously) Download image from given URI and save in output path
        /// </summary>
        /// <param name="uri">Image URI</param>
        /// <param name="outputDirectoryPath">Directory path to store the image</param>
        public static async Task<int> DownloadFileAsync(List<string> lUri, string outputDirectoryPath)
        {
            int i = 0;

            foreach (var uri in lUri)
            {
                //Check of the Image URI is valid
                if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult))
                    throw new InvalidOperationException("Image URI is invalid.");

                //Fetch the Image in bytes[]
                byte[] fileBytes = await client.GetByteArrayAsync(uriResult);

                //split the URI to fetch the image name
                var uriSplit = uri.Split('/');

                try
                {
                    //Write the image to a file
                    File.WriteAllBytes(outputDirectoryPath + uriSplit[uriSplit.Length - 1], fileBytes);
                    i++;
                }
                catch (Exception)
                {
                    throw;
                }                

            }

            return i;
        }
    }
}
