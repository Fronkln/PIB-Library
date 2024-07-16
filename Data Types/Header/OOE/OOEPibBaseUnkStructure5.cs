using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Are these UV?
    public class OOEPibBaseUnkStructure5
    {
        public float Unknown1 = 0;
        public float Unknown2 = 1;
        public float Unknown3 = 0;
        public float Unknown4 = 1;
        public float Unknown5 = 0;
        public float Unknown6 = 0;
        public float Unknown7 = 0;
        public float Unknown8 = 0;
        public float Unknown9 = 1;
        public float Unknown10 = 0;
        public float Unknown11 = 0;
        public float Unknown12 = 0;
        public float Unknown13 = 0;
        public float Unknown14 = 0;
        public float Unknown15 = 0;

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
            Unknown11 = reader.ReadSingle();
            Unknown12 = reader.ReadSingle();
            Unknown13 = reader.ReadSingle();
            Unknown14 = reader.ReadSingle();
            Unknown15 = reader.ReadSingle();
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
            writer.Write(Unknown11);
            writer.Write(Unknown12);
            writer.Write(Unknown13);
            writer.Write(Unknown14);
            writer.Write(Unknown15);
        }
    }
}
