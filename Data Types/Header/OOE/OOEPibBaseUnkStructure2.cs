using System;

using Yarhl.IO;

namespace PIBLib
{
    public class OOEPibBaseUnkStructure2
    {
        public float Unknown1;
        public float Unknown2;
        public float Unknown3;
        public float Unknown4;
        public float Unknown5;
        public float Unknown6;
        public float Unknown7;
        public float Unknown8;
        public float Unknown9;
        public float Unknown10;
        public float Unknown11;
        public float Unknown12;
        public float Unknown13;
        public float Unknown14;
        public float Unknown15;

        public virtual void Read(DataReader reader)
        {
            Unknown1 = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            Unknown4 = reader.ReadSingle();
            Unknown5 = reader.ReadSingle();
            Unknown6 = reader.ReadSingle();
            Unknown7 = reader.ReadSingle();
            Unknown8 = reader.ReadSingle();
            Unknown9 = reader.ReadSingle();
            Unknown10 = reader.ReadSingle();
        }

        public virtual void Write(DataWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
            writer.Write(Unknown5);
            writer.Write(Unknown6);
            writer.Write(Unknown7);
            writer.Write(Unknown8);
            writer.Write(Unknown9);
            writer.Write(Unknown10);
        }
    }
}
