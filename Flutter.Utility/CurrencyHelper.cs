using System;
using System.Linq;

namespace Flutter.Utility
{
    public class CurrencyHelper
    {
        /// <summary>
        /// Return a currency symbol based on a 3 digit currency code being passed e.g EUR, GBP, USD
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetCurrencySymbol(string code)
        {
            System.Globalization.RegionInfo regionInfo = (from culture in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.InstalledWin32Cultures)
                                                          where culture.Name.Length > 0 && !culture.IsNeutralCulture
                                                          let region = new System.Globalization.RegionInfo(culture.LCID)
                                                          where String.Equals(region.ISOCurrencySymbol, code, StringComparison.InvariantCultureIgnoreCase)
                                                          select region).First();

            return regionInfo.CurrencySymbol;
        }

    }
}
