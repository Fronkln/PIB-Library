using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class OOEPibBaseUnkStructure6
    {
        public int Unknown1;
        public float Unknown2;
        public float Unknown3;
        public int Unknown4;
        public float Unknown5;
        public int Flag4;

        public virtual void Read(DataReader reader)
        {
            Unknown1 = reader.ReadInt32();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            Unknown4 = reader.ReadInt32();
            Unknown5 = reader.ReadSingle();
            Flag4 = reader.ReadInt32();
        }

        public virtual void Write(DataWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
            writer.Write(Unknown5);
            writer.Write(Flag4);
        }
    }
}
