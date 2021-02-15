
using System.Collections.Generic;
using Flutter.Core.Models;
using System.Threading.Tasks;

namespace Flutter.CodingTest.Interfaces
{
    public interface IBetDataApiService
    {
        /// <summary>
        /// Makes a call to an API and deseralizes the response into an enumerable object
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<IEnumerable<BetData>> GetAsync(string url);
    }
}