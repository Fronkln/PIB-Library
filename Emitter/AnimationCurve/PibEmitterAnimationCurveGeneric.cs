using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterAnimationCurveGeneric : PibEmitterAnimationCurve
    {
        public float[] Values;

        internal override void Read(DataReader reader, int numfloats)
        {
            Values = new float[numfloats];

            for (int i = 0; i < numfloats; i++)
                Values[i] = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            foreach (float f in Values)
                writer.Write(f);
        }

        internal override int GetDataSize()
        {
            return Values.Length * 4;
        }
    }
}
