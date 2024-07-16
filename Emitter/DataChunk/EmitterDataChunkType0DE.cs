using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{

    //GEO VTX
    public class EmitterDataChunkType0DE : EmitterBaseDataChunk
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            Data = new float[70 / 4];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadSingle();
        }
    }
}
