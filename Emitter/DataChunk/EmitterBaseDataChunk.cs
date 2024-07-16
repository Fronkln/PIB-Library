using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterBaseDataChunk
    {
        public float[] Data;

        internal virtual void Write(DataWriter writer)
        {
            foreach (float f in Data)
                writer.Write(f);
        }

        internal virtual void Read(DataReader reader, PibVersion version)
        {

        }
    }
}
