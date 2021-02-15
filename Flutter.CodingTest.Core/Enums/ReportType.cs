using System.ComponentModel;

namespace Flutter.CodingTest
{
    public enum ReportType
    {
        [Description("Report Showing Selection Liabilty by Currency")]
        Report1 = 0,
        [Description("Report Showing Total Liability by Currency")]
        Report2 = 1,
    }
}