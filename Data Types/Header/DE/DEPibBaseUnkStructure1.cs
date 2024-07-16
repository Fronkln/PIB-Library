using System;
using Yarhl.IO;

namespace PIBLib
{
    //Ishin: Introduced, 32 bytes
    //Y6: Values changed places, appears after the 12 bytes of padding that came after Track
    //YK2: Moved to after Light UI
    //YLAD: Moved places, comes after things like extra texture count.
    //LJ: Starts 8 bytes late.
    public class PibBaseUnkStructure1
    {
        public float Unk1 = 6000f;
        public float Unk2 = 15030f;
        public float Unk3 = 1f;
        public float Unk4 = 15000f;
        public float Unk5 = 1f;
        public float Unk6 = 1f;
        public float Unk7 = 1f;
        public float Unk8 = 1f;

        internal virtual void Read(DataReader reader)
        {
            Unk1 = reader.ReadSingle();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Unk4 = reader.ReadSingle();
            Unk5 = reader.ReadSingle();
            Unk6 = reader.ReadSingle();
            Unk7 = reader.ReadSingle();
            Unk8 = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Unk1);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Unk4);
            writer.Write(Unk5);
            writer.Write(Unk6);
            writer.Write(Unk7);
            writer.Write(Unk8);
        }
    }
}
