using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterDataChunkType0 : EmitterBaseDataChunk
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            Data = new float[32 / 4];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadSingle();
        }
    }
}
