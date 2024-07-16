using System;
using Yarhl.IO;

namespace PIBLib
{
    public class OOEPibUnkStructure1v21 : OOEPibBaseUnkStructure1
    {
        public float Unknown5;
        public float Unknown6;
        public float Unknown7;
        public float Unknown8;
        internal override void Read(DataReader reader)
        {
            base.Read(reader);

            Unknown5 = reader.ReadSingle();
            Unknown6 = reader.ReadSingle();
            Unknown7 = reader.ReadSingle();
            Unknown8 = reader.ReadSingle();
        }

        internal override void Write(DataWriter writer)
        {
            base.Write(writer);

            writer.Write(Unknown5);
            writer.Write(Unknown6);
            writer.Write(Unknown7);
            writer.Write(Unknown8);
        }

    }
}
