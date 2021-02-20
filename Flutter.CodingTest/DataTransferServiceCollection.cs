
using Flutter.CodingTest.Interfaces;
using Flutter.CodingTest.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Flutter.CodingTest
{
    public static class DataTransferServiceCollection
    {
        public static void AddDataTransferServices(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IReportGeneratorService, ReportGeneratorService>();
            services.AddSingleton<IBetDataApiService, BetDataApiService>();
        }
    }
}
