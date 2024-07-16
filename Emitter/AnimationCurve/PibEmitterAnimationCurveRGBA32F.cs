using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterAnimationCurveRGBA32F : PibEmitterAnimationCurve
    {
        public RGBA32F[] Values;

        internal override void Read(DataReader reader, int numfloats)
        {
            int numColors = numfloats / 4;

            Values = new RGBA32F[numColors];

            for(int i = 0; i < Values.Length; i++)
                Values[i] = reader.Read<RGBA32F>();
        }

        internal override void Write(DataWriter writer)
        {
            foreach (RGBA32F f in Values)
            {
                writer.Write(f.R);
                writer.Write(f.G);
                writer.Write(f.B);
                writer.Write(f.A);
            }
        }

        internal override int GetDataSize()
        {
            return Values.Length * 4;
        }
    }
}
