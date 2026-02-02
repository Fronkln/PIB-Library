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

        public Vector2[] TextureFrameSizes;

        public virtual void Read(DataReader reader)
        {
            TickUnknown1 = reader.ReadSingle();
            TickUnknown2 = reader.ReadSingle();

            FrameRelated1 = reader.ReadSingle();
            FrameRelated2 = reader.ReadSingle();

            TextureFrames = new float[2];
            TextureWidths = new float[2];
            TextureFrameSizes = new Vector2[2];

            for(int i = 0; i < 2; i++)
                TextureFrames[i] = reader.ReadSingle();

            for(int i = 0; i < 2; ++i) 
                TextureWidths[i] = reader.ReadSingle();

            for (int i = 0; i < 2; ++i)
                TextureFrameSizes[i] = reader.ReadVector2();
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

            for (int i = 0; i < 2; i++)
                writer.Write(TextureFrameSizes[i]);
        }

        public EmitterAnimationDataDE ToDE()
        {
            EmitterAnimationDataDE deDat = new EmitterAnimationDataDE();

            deDat.TickUnknown1 = TickUnknown1;
            deDat.TickUnknown2 = TickUnknown2;

            deDat.FrameRelated1 = FrameRelated1;
            deDat.FrameRelated2 = FrameRelated2;

            deDat.TextureFrameSizes = new Vector2[4];
            deDat.TextureFrameSizes[0] = TextureFrameSizes[0];
            deDat.TextureFrameSizes[1] = TextureFrameSizes[1];

            deDat.TextureFrames = new float[4] { 1, 1, 1, 1 };
            deDat.TextureFrames[0] = TextureFrames[0];
            deDat.TextureFrames[1] = TextureFrames[1];

            deDat.TextureWidths = new float[4] { 1, 1, 1, 1 };
            deDat.TextureWidths[0] = TextureWidths[0];
            deDat.TextureWidths[1] = TextureWidths[1];

            deDat.UnkTextureData1 = new float[4] { 1, 1, 1, 1 };
            deDat.TextureSpeeds = new float[4];

            return deDat;
        }
    }
}
