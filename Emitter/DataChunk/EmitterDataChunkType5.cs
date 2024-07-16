using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterDataChunkType5 : EmitterBaseDataChunk
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            if (version < PibVersion.YLAD)
                Data = new float[28 / 4];
            else
                Data = new float[44 / 4];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadSingle();
        }

        public EmitterDataChunkType1 ToType1()
        {
            EmitterDataChunkType1 type = new EmitterDataChunkType1();
            type.Data = new float[11];

            for (int i = 0; i < 7; i++)
                type.Data[i] = Data[i];

            return type;
        }
    }
}
