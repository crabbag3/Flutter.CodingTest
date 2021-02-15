using Flutter.CodingTest.Interfaces;
using Flutter.CodingTest.Models;
using Flutter.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.CodingTest.Services
{
    public class BetDataApiService: IBetDataApiService
    {
        private readonly HttpClient _httpClient;

        public BetDataApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Makes a call to an API and deseralizes the response into an enumerable object
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BetData>> GetAsync(string url)
        { 
            
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // Auth required?

            var response = await _httpClient.SendAsync(request);

            var resultString = await response.Content.ReadAsStringAsync();
            return (IEnumerable<BetData>)JsonConvert.DeserializeObject<BetData>(resultString);

        }
    }
}
