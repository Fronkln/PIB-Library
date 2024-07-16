using Yarhl.IO;

namespace PIBLib
{
    //Y3: Added
    //Y5: Data extended by 16 bytes
    //Ishin: Removed
    public class OOEPibBaseUnkStructure1
    {
        public float Unknown1 = 566;
        public float Unknown2 = 1f;
        public float Unknown3 = 0.25f;
        public float Unknown4 = 0.02f;

        internal virtual void Read(DataReader reader)
        {
            Unknown1 = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            Unknown4 = reader.ReadSingle();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
        }
    }
}
