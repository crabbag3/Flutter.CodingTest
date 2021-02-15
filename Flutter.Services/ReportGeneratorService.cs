using Flutter.CodingTest.Models;
using System;

using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Flutter.CodingTest.Interfaces;
using Flutter.Core.Models;

namespace Flutter.CodingTest.Services
{
    public class ReportGeneratorService : IReportGeneratorService
    {
        private readonly IBetDataApiService _betDataApiService;

        public ReportGeneratorService(IBetDataApiService betDataApiService)
        {
            _betDataApiService = betDataApiService;
        }

        /// <summary>
        /// Generates a report and returns in as ReportResponseModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IEnumerable<ReportModel> GenerateReport1(IEnumerable<BetData> data)
        {
            return data.GroupBy(x => new { x.SelectionName, x.Currency }).Select(x =>
            new ReportModel
            {
                SelectionName = x.Key.SelectionName,
                Currency = x.Key.Currency,
                NumberOfBets = data.Where(y => (y.SelectionName == x.Key.SelectionName) && (y.Currency == x.Key.Currency)).Count(),
                TotalStakes = data.Where(y => (y.SelectionName == x.Key.SelectionName) && (y.Currency == x.Key.Currency)).Sum(y => y.Stake),
                TotalLiability = data.Where(y => (y.SelectionName == x.Key.SelectionName) && (y.Currency == x.Key.Currency)).Sum(y => y.Stake * y.Price)
            }).OrderByDescending(z => z.Currency).ThenByDescending(z => z.TotalLiability);
        }

        /// <summary>
        /// Generates a report and returns it as a list 
        /// </summary>
        public IEnumerable<ReportModel> GenerateReport2(IEnumerable<BetData> data)
        {
            return data.GroupBy(x => x.Currency).Select(x =>
            new ReportModel
            {
                Currency = x.Key,
                NumberOfBets = data.Where(y => y.Currency == x.Key).Count(),
                TotalStakes = data.Where(y => y.Currency == x.Key).Sum(y => y.Stake),
                TotalLiability = data.Where(y => y.Currency == x.Key).Sum(y => y.Stake * y.Price)
            });
        }

        /// <summary>
        /// Calls the BetDataApi service to make an API call
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BetData>> GetBetDataApi(string filePath)
        {
            return await _betDataApiService.GetAsync(filePath);
        }
    }
    }
