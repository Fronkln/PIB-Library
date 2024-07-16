using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterDataChunkType1 : EmitterBaseDataChunk
    {
        public EmitterDataChunkType1()
        {
            Data = new float[44 / 4];
        }
        internal override void Read(DataReader reader, PibVersion version)
        {
            Data = new float[44 / 4];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadSingle();
        }
    }
}
