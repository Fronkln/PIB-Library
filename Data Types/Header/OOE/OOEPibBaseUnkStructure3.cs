using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Y3: Added
    //Ishin: Removed
    public class OOEPibBaseUnkStructure3
    {
        public float Unk1 = 1;
        public float Unk2 = 1;
        public float Unk3 = 1;
        public float Unk4 = 3000;

        internal virtual void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
        }
    }
}
