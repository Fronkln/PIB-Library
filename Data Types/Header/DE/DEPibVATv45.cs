using Yarhl.IO;

namespace PIBLib
{
    [Yarhl.IO.Serialization.Attributes.Serializable]
    public class DEPibVATv45 : DEPibBaseVAT
    {
        public double Bbox1Length;
        public double Bbox1Min;

        internal override void Read(DataReader reader)
        {
            BboxLength = reader.ReadDouble();
            BboxMin = reader.ReadDouble();
            
            Bbox1Length = reader.ReadDouble();
            Bbox1Min = reader.ReadDouble();
            
            Frames = reader.ReadUInt32();
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(BboxLength);
            writer.Write(BboxMin);

            writer.Write(Bbox1Length);
            writer.Write(Bbox1Min);

            writer.Write(Frames);
        }
    }
}
