using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal static class Extensions
    {
        public static string ToLength(this string self, int length)
        {
            if (self == null)
                return null;

            if (self.Length == length)
                return self;

            if (self.Length > length)
                return self.Substring(0, length);


            StringBuilder str = new StringBuilder();
            str.Append(self);

            while (str.Length != length)
                str.Append('\0');

            return str.ToString();
        }

        public static string SplitOnCapitals(this string text)
        {
            string newstring = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    newstring += " ";
                newstring += text[i].ToString();
            }

            return newstring;
        }
    }
}
