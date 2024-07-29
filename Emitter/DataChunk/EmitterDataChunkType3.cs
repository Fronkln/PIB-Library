using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterDataChunkType3 : EmitterBaseDataChunk
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            if (version < PibVersion.YLAD)
                Data = new float[40 / 4];
            else
                Data = new float[44 / 4];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadSingle();
        }

        public EmitterDataChunkType1 ToType1()
        {
            EmitterDataChunkType1 type1 = new EmitterDataChunkType1();
            type1.Data = new float[44 / 4];

            for (int i = 0; i < Data.Length; i++)
                type1.Data[i] = Data[i];

            return type1;
        }
    }
}
