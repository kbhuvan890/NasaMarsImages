using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NasaMarsImageServices.ServiceLogic
{
    internal class ApiServiceLogic
    {

        //baseUri : https://api.nasa.gov/
        //endpoint path : mars-photos/api/v1/rovers/curiosity/photos
        //param : earth_date=2015-6-3&api_key=NC2AZKxmFDppkoHOy3dKg3Yzd8NHsFtT5RzuO7wc

        private readonly HttpClient _client;

        public ApiServiceLogic(string baseUri)
        {
             //initilize httpclinet object .
            _client = new HttpClient((new HttpClientHandler() { UseDefaultCredentials = true }))
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        /// <summary>
        /// Use this method to call an API service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Base URI</param>
        /// <param name="endpointpath">Endpoint URI</param>
        /// <param name="queryParams">Query String Parameters</param>
        /// <returns></returns>
        internal T CallService<T>(string endpointpath, Dictionary<string, string> queryParams = null)
        {
            //Add Query string to the endpoint and pass to the http client
            var response = _client.GetAsync(QueryHelpers.AddQueryString(endpointpath, queryParams)).Result;

            //Check for response status
            if (response.IsSuccessStatusCode)
            {
                //Read the response contents and deserialize it based on object type.
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {   
                //throw the response when its false
                throw new HttpRequestException(response.ToString());
            }

        }

    }
}
