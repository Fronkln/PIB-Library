using Yarhl.IO;

namespace PIBLib
{
    public class EmitterBaseAnimationData
    {
        public float TickUnknown1;
        public float TickUnknown2;
        
        public float FrameRelated1;
        public float FrameRelated2;
       
        public float[] TextureFrames;
        public float[] TextureWidths;

        public float UnknownX;
        public float UnknownY;

        public virtual void Read(DataReader reader)
        {
            TickUnknown1 = reader.ReadSingle();
            TickUnknown2 = reader.ReadSingle();

            FrameRelated1 = reader.ReadSingle();
            FrameRelated2 = reader.ReadSingle();

            TextureFrames = new float[2];
            TextureWidths = new float[2];

            for(int i = 0; i < 2; i++)
                TextureFrames[i] = reader.ReadSingle();

            for(int i = 0; i < 2; ++i) 
                TextureWidths[i] = reader.ReadSingle();

            UnknownX = reader.ReadSingle();
            UnknownY = reader.ReadSingle();
        }

        public virtual void Write(DataWriter writer)
        {
            writer.Write(TickUnknown1);
            writer.Write(TickUnknown2);

            writer.Write(FrameRelated1);
            writer.Write(FrameRelated2);

            for(int i = 0; i < 2; i++)
                writer.Write(TextureFrames[i]);

            for(int i = 0; i < 2; i++)
                writer.Write(TextureWidths[i]);

            writer.Write(UnknownX);
            writer.Write(UnknownY);
        }
    }
}
