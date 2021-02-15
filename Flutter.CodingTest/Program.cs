using ConsoleTables;
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
            IServiceCollection services = new ServiceCollection();

            services.AddDataTransferServices();

            using var serviceProvider = services.BuildServiceProvider();
            var dataTransfer = serviceProvider.GetService<ReportGeneratorService>();

            Console.OutputEncoding = System.Text.Encoding.Unicode; // allow printing of unicode character i.e € symbol

            Console.WriteLine("Hello Welcome to....... ");
            Console.WriteLine("Please copy the file path to where the CSV is stored or copy the http link where the data can be requested");

            string userInput = Console.ReadLine();
            List<BetData> betData = new List<BetData>();

            if (userInput.Length >= 3 && userInput.EndsWith(".csv")) // check is csv
            {
                // TODO: Add exception handling
                Console.WriteLine("Trying to read data from the csv file..");
                betData = CsvParser.ParseCsv<BetData>(userInput);
                //Success message here
            }
            if (userInput.Contains("http"))
            {
                try
                {
                    Console.WriteLine($"Data will be requested from {userInput}");
                    // validate that you can make request
                    betData = (List<BetData>)await dataTransfer.GetBetDataApi(userInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occured when trying to obtain data from {userInput}\n{ex}");
                    throw;
                }
            }

            // Type of Report to be Generated

            Console.WriteLine("\nHow would you like the data outputted?" +
                "\nPress 1 for it to be outputed to the console or 2 for it to written to a CSV file");

            string input = Console.ReadLine();
            if (input.Equals("1"))
            {
                ReportType reportType = TypeOfReport();
                if (reportType == ReportType.Report1)
                {
                    var report1 = dataTransfer.GenerateReport1(betData);
                    // make method here for generating console report
                    ConsoleTable
                            
                          .From(report1.Select(x => new Report1
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
                    var report2 = dataTransfer.GenerateReport2(betData);
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
                // confirm you want to overwrite
                string fileName = $"{reportType}bet_data_ouput.csv";

                if (reportType == ReportType.Report1)
                {
                    var report1 = dataTransfer.GenerateReport1(betData);
                    CsvParser.CreateCsv(report1.ToList(), fileName);
                }
                if (reportType == ReportType.Report2)
                {
                    var report2 = dataTransfer.GenerateReport2(betData);
                    CsvParser.CreateCsv(report2.ToList(), fileName);
                }

                // CHANGE WHERE FILE IS OUTPUTTED
                Console.WriteLine($"The file will be generated in {Directory.GetCurrentDirectory()}/{reportType}bet_data_output.csv");
            }
            // error handling?
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
            ReportType reportType = new ReportType();
            Console.WriteLine($"\nPress 1 to print a report showing selection liability by currency");
            Console.WriteLine($"Press 2 to print a report showing total liability by Currency");
            string input = Console.ReadLine();
            if (input != "1" && input != "2")
            {
                Console.WriteLine("Wrong option selected");
                Console.WriteLine("Press any key to exit..");
                Console.ReadKey();
                Environment.Exit(0); // exit app
                //throw er?
            }
            if (input == "1")
            {
                reportType = ReportType.Report1;
            }
            if (input == "2")
            {
                reportType = ReportType.Report2; //make into method here??
            }
            return reportType;
        }
    }
}