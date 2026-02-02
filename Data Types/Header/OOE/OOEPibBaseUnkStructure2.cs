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

        public virtual void Read(DataReader reader)
        {
            Unknown1 = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            Unknown4 = reader.ReadSingle();
        }

        public virtual void Write(DataWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
        }
    }
}
