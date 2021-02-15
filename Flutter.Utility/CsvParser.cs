using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Flutter.CodingTest
{
    public class CsvParser
    {
        /// <summary>
        /// Generic method that parses a CSV file and returns a list of RequestedTypes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<T> ParseCsv<T>(string filePath)
        {
            List<T> betData = new List<T>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = (string header, int index) => header.Trim(),
                
            };
            using (TextReader reader = File.OpenText(filePath))
            {
                CsvReader csv = new CsvReader(reader, config);
                while (csv.Read())
                {
                    
                    T Record = csv.GetRecord<T>();
                    betData.Add(Record);
                }
            }
            return betData;
        }

        /// <summary>
        /// Generic method to create a new csv file from the object passed to it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="fileName"></param>
        public static void CreateCsv<T>(List<T> input, string fileName)
        {
            // Create a new file     
            using (FileStream fs = File.Create(fileName))
            {

            }

            using var writer = new StreamWriter(fileName);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(input);

        }

    }
}
