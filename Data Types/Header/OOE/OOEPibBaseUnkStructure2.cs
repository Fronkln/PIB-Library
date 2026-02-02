using System;

using Yarhl.IO;

namespace PIBLib
{
    public class OOEPibBaseUnkStructure2
    {
        public float Unknown1;
        public float Unknown2;

        public virtual void Read(DataReader reader)
        {
            Unknown1 = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
        }

        public virtual void Write(DataWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write(Unknown2);
        }
    }
}
