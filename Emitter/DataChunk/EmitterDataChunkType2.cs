using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
 
    //Terrible news, this was changed to 44 in YLAD and above
    //But i had to convert type 2 to type 1 for Y0 to Y6 because it was crashing, maybe it was actually changed in Yakuza 6? (v29)
    public class EmitterDataChunkType2 : EmitterBaseDataChunk
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            if (version < PibVersion.YLAD)
                Data = new float[20 / 4];
            else
                Data = new float[44 / 4];

            for (int i = 0; i < Data.Length; i++)
                Data[i] = reader.ReadSingle();
        }

        public EmitterDataChunkType1 ToType1()
        {
            EmitterDataChunkType1 type = new EmitterDataChunkType1();
            type.Data = new float[11];

            for (int i = 0; i < 5; i++)
                type.Data[i] = Data[i];

            type.Data[5] = type.Data[3];
            type.Data[6] = type.Data[4];
            type.Data[7] = type.Data[3];
            type.Data[8] = type.Data[4];
            type.Data[9] = type.Data[3];
            type.Data[10] = type.Data[4];

            return type;
        }
    }
}
