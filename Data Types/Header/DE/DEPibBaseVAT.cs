using Yarhl.IO;

namespace PIBLib
{
    public class DEPibBaseVAT
    {
        public double BboxLength = 2;
        public double BboxMin = -1;
        public uint Frames = 100;

        internal virtual void Read(DataReader reader)
        {
            BboxLength = reader.ReadSingle();
            BboxMin = reader.ReadSingle();
            Frames = reader.ReadUInt32();
        }

        internal virtual void Write(DataWriter writer)
        {
            writer.Write((float)BboxLength);
            writer.Write((float)BboxMin);
            writer.Write(Frames);
        }
    }
}
