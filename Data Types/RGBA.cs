using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public struct RGBA
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
      
        public RGBA(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public int MergeToInt()
        {
            byte[] bytes = new byte[] { R, G, B, A };

            // Use bitwise operations to merge bytes into an integer
            int mergedInt = 0;
            mergedInt |= bytes[0];                  // Merge first byte
            mergedInt <<= 8;                        // Shift left by 8 bits
            mergedInt |= bytes[1];                  // Merge second byte
            mergedInt <<= 8;                        // Shift left by 8 bits
            mergedInt |= bytes[2];                  // Merge third byte
            mergedInt <<= 8;                        // Shift left by 8 bits
            mergedInt |= bytes[3];                  // Merge fourth byte

            return mergedInt;
        }

        public static implicit operator System.Drawing.Color(RGBA rgba)
        {
            System.Drawing.Color col = System.Drawing.Color.FromArgb(rgba.A, rgba.R, rgba.G, rgba.B);
            return col;
        }

        public static implicit operator RGBA(System.Drawing.Color rgba)
        {
            return new RGBA(rgba.R, rgba.G, rgba.B, rgba.A);
        }
    }
}
