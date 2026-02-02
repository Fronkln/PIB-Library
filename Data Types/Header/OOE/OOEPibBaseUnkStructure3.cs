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
        public float Duration = 3000; //Usually the particles in the emitters have this valeu as well.

        internal virtual void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Duration = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Duration);
        }
    }
}
