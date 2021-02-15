using CsvHelper.Configuration.Attributes;
using Flutter.CodingTest.Models;

namespace Flutter.Core.Models
{
    public class BetData
    {
        [Name("betId")]
        public string _id { get; set; }

        [Name("betTimestamp")]
        public long Ticks { get; set; }
        [Name("selectionId")]
        public string SelectionId { get; set; }
        [Name("selectionName")]

        public string SelectionName { get; set; }
        [Name("stake")]
        public double Stake { get; set; }
        [Name("price")]
        public double Price { get; set; }
        [Name("currency")]
        public Currency Currency { get; set; }

    }

}
