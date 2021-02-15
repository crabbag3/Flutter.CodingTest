using System.ComponentModel;

namespace Flutter.CodingTest.Models
{
    /// <summary>
    /// Enum to indicate what currency is used
    /// </summary>
    public enum Currency
    {
        [Description("Euro")]
        EUR = 0,
        [Description("Gbp")]
        GBP = 1,
        [Description("Usd")]
        USD = 2,

    }
}