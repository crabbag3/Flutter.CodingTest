using ConsoleTables;
using Flutter.CodingTest.Interfaces;
using Flutter.CodingTest.Models;
using Flutter.CodingTest.Services;
using Flutter.Core.Models;
using Flutter.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Flutter.CodingTest
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            #region Dependencies
            IServiceCollection services = new ServiceCollection();
            services.AddDataTransferServices();
            using var serviceProvider = services.BuildServiceProvider();

            var _reportGeneratorService = serviceProvider.GetService<IReportGeneratorService>();

            #endregion

            Console.OutputEncoding = System.Text.Encoding.Unicode; // allow printing of unicode character i.e € symbol

            Console.WriteLine("Hello Welcome to....... ");
            Console.WriteLine("Please copy the file path to where the CSV is stored or copy the http link where the json data can be requested");

            string userInput = Console.ReadLine();
            List<BetData> betData = new List<BetData>();

            if (userInput.Length >= 3 && userInput.EndsWith(".csv")) // check is csv
            {
                Console.WriteLine("\nTrying to read data from the csv file..");
                betData = CsvParser.ParseCsv<BetData>(userInput);
            }
            else if (userInput.Contains("http"))
            {

                Console.WriteLine($"JSON data will be requested from {userInput}");
                betData = (List<BetData>)await _reportGeneratorService.GetBetDataApi(userInput);
            }
            else
            {
                throw new ArgumentException("Invalid argument - please ensure the path is correct or the HTTP link is an accessible endpoint");
            }


            // Type of Report to be Generated
            Console.WriteLine("\nHow would you like the data outputed?" +
                "\nPress 1 for it to be outputed to the console or 2 for it to written to a CSV file");

            string input = Console.ReadLine();
            if (input.Equals("1"))
            {
                ReportType reportType = TypeOfReport();
                if (reportType == ReportType.Report1)
                {

                    // make method here for generating console report
                    ConsoleTable

                          .From(_reportGeneratorService.GenerateReport1(betData).Select(x => new Report1
                          {
                              Currency = x.Currency.ToString(),
                              SelectionName = x.SelectionName,
                              TotalStakes = $"{CurrencyHelper.GetCurrencySymbol(x.Currency.ToString())}{x.TotalStakes:F}",
                              TotalLiability = $"{CurrencyHelper.GetCurrencySymbol(x.Currency.ToString())}{x.TotalLiability:F}",
                              NumberOfBets = x.NumberOfBets
                          }))

                          .Configure(o => o.NumberAlignment = Alignment.Right)
                         .Write(Format.Alternative);
                }
                if (reportType == ReportType.Report2)
                {
                    var report2 = _reportGeneratorService.GenerateReport2(betData);
                    ConsoleTable
                           .From(report2.Select(x => new Report2
                           {
                               Currency = x.Currency,
                               NumOfBets = x.NumberOfBets,
                               TotalStakes = $"{CurrencyHelper.GetCurrencySymbol(x.Currency.ToString())}{x.TotalStakes:F}",
                               TotalLiability = $"{CurrencyHelper.GetCurrencySymbol(x.Currency.ToString())}{x.TotalLiability:F}",
                           }))
                        .Configure(o => o.NumberAlignment = Alignment.Right)
                        .Write(Format.Alternative);
                }
                Console.WriteLine("Table outputted to console");
                Console.WriteLine("Press any key to exit..");
                Console.ReadLine();
                Environment.Exit(0); // exit app
            }
            else if (input.Equals("2"))
            {
                ReportType reportType = TypeOfReport();
                // TODO: Confirm you want to overwrite
                string fileName = $"{reportType}bet_data_ouput.csv";

                if (reportType == ReportType.Report1)
                {
                    var report1 = _reportGeneratorService.GenerateReport1(betData);
                    CsvParser.CreateCsv(report1.ToList(), fileName);
                }
                if (reportType == ReportType.Report2)
                {
                    var report2 = _reportGeneratorService.GenerateReport2(betData);
                    CsvParser.CreateCsv(report2.ToList(), fileName);
                }


                Console.WriteLine($"The file will be generated in {Directory.GetCurrentDirectory()}/{reportType}bet_data_output.csv");
            }

            else Console.WriteLine("You didn't select a valid option\nExiting...");
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
            Environment.Exit(0); // exit app
        }

        /// <summary>
        /// Method to return the report type selected based on user input
        /// </summary>
        /// <returns></returns>
        public static ReportType TypeOfReport()
        {
            Console.WriteLine($"\nPress 1 to print a report showing selection liability by currency");
            Console.WriteLine($"Press 2 to print a report showing total liability by Currency");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    return ReportType.Report1;

                case "2":
                    return ReportType.Report2;

            }
            throw new ArgumentException("Invalid Report Selected: ", input);
        }
    }
}