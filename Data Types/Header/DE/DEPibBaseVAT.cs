using Yarhl.IO;

namespace PIBLib
{
    public class DEPibBaseVAT
    {
        public double BboxLength;
        public double BboxMin;
        public uint Frames;

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
