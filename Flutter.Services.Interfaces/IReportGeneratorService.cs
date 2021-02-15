using Flutter.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flutter.CodingTest.Interfaces
{
    public interface IReportGeneratorService
    {
        /// <summary>
        /// Generates a report and returns it as a ReportModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IEnumerable<ReportModel> GenerateReport1(IEnumerable<BetData> data);
        /// <summary>
        /// Generates a report and returns it as a ReportModel 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IEnumerable<ReportModel> GenerateReport2(IEnumerable<BetData> data);

        /// <summary>
        /// Calls the BetDataApi service to make an API call
        /// </summary
        /// <param name="url"></param>
        /// <returns></returns>
        Task<IEnumerable<BetData>> GetBetDataApi(string url);

    }
}
