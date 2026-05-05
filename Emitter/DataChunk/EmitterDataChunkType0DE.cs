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
        public byte[] UnknownData;

        public EmitterDataChunkType0DE()
        {
            UnknownData = new byte[24];
        }

        internal override void Read(DataReader reader, PibVersion version)
        {
            base.Read(reader, version);

            UnknownData = new byte[24];

            for (int i = 0; i < UnknownData.Length; i++)
                UnknownData[i] = reader.ReadByte();
        }

        internal override void Write(DataWriter writer)
        {
            base.Write(writer);

            writer.Write(UnknownData);
        }
    }
}
