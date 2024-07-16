using System;
using System.Globalization;

namespace PIBView
{
    internal static class Utils
    {
        public static float InvariantParse(string val)
        {
            return float.Parse(val, CultureInfo.InvariantCulture);
        }
    }
}
