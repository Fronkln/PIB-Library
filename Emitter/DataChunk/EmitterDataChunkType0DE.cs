using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{

    //Same data as Type 1? Extra data was all zeroes??? bsl0001
    public class EmitterDataChunkType0DE : EmitterDataChunkType1
    {
        public Vector2 Unknown1;
        public Vector2 Unknown2;
        public Vector2 Unknown3;

        internal override void Read(DataReader reader, PibVersion version)
        {
            base.Read(reader, version);

            Unknown1 = reader.ReadVector2();
            Unknown2 = reader.ReadVector2();
            Unknown3 = reader.ReadVector2();
        }

        internal override void Write(DataWriter writer)
        {
            base.Write(writer);

            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
        }
    }
}
