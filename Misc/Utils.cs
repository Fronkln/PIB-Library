using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    internal static class Utils
    {
        public static void AssertEmpty(this byte[] dat)
        {
            for(int i = 0; i < dat.Length; i++)
            {
                Debug.Assert(dat[i] == 0, $"This is not padding as you previously thought! Idx: {i}");
            }
        }
    }
}
