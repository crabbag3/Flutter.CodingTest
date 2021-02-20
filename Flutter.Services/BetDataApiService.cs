using Flutter.CodingTest.Interfaces;
using Flutter.CodingTest.Models;
using Flutter.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.CodingTest.Services
{
    public class BetDataApiService : IBetDataApiService
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
            IEnumerable<BetData> betData = new List<BetData>();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var httpResponse = await _httpClient.SendAsync(request);
            
            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Http exception occured: {httpResponse.StatusCode}");
            }

           var response = await httpResponse.Content.ReadAsStringAsync();
           betData = (IEnumerable<BetData>)JsonConvert.DeserializeObject<BetData>(response);
           
           return betData;
        }
    }
}
