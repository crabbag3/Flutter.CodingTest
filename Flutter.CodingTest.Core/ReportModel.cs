using Flutter.CodingTest.Models;

namespace Flutter.Core.Models
{
    public class ReportModel
    {
        public string SelectionName { get; set; }
        public Currency Currency { get; set; }
        public int NumberOfBets { get; set; }
        public double TotalStakes { get; set; }
        public double TotalLiability { get; set; }
    }
}