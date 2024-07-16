using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class EmitterAnimationDataDE : EmitterBaseAnimationData
    {
        public float[] UnkTextureData1;
        public float[] UnkTextureData2;

        public override void Read(DataReader reader)
        {
            TickUnknown1 = reader.ReadSingle();
            TickUnknown2 = reader.ReadSingle();

            FrameRelated1 = reader.ReadSingle();
            FrameRelated2 = reader.ReadSingle();

            TextureFrames = new float[4];
            TextureWidths = new float[4];
            UnkTextureData1 = new float[4];
            UnkTextureData2 = new float[4];


            for (int i = 0; i < 4; ++i)
                UnkTextureData1[i] = reader.ReadSingle();

            for (int i = 0; i < 4; i++)
                TextureFrames[i] = reader.ReadSingle();

            for (int i = 0; i < 4; ++i)
                TextureWidths[i] = reader.ReadSingle();

            for (int i = 0; i < 4; ++i)
                UnkTextureData2[i] = reader.ReadSingle();

            UnknownX = reader.ReadSingle();
            UnknownY = reader.ReadSingle();
        }

        public override void Write(DataWriter writer)
        {
            writer.Write(TickUnknown1);
            writer.Write(TickUnknown2);

            writer.Write(FrameRelated1);
            writer.Write(FrameRelated2);

            for (int i = 0; i < 4; i++)
                writer.Write(UnkTextureData1[i]);

            for (int i = 0; i < 4; i++)
                writer.Write(TextureFrames[i]);

            for (int i = 0; i < 4; i++)
                writer.Write(TextureWidths[i]);

            for (int i = 0; i < 4; i++)
                writer.Write(UnkTextureData2[i]);

            writer.Write(UnknownX);
            writer.Write(UnknownY);
        }

        public EmitterBaseAnimationData ToOE()
        {
            EmitterBaseAnimationData oeDat = new EmitterBaseAnimationData();
            oeDat.TickUnknown1 = TickUnknown1;
            oeDat.TickUnknown2 = TickUnknown2;

            oeDat.FrameRelated1 = FrameRelated1;
            oeDat.FrameRelated2 = FrameRelated2;

            oeDat.TextureFrames = new float[2];
            oeDat.TextureFrames[0] = TextureFrames[0];
            oeDat.TextureFrames[1] = TextureFrames[1];

            oeDat.TextureWidths = new float[2];
            oeDat.TextureWidths[0] = TextureWidths[0];
            oeDat.TextureWidths[1] = TextureWidths[1];

            oeDat.UnknownX = UnknownX;
            oeDat.UnknownY = UnknownY;

            oeDat.FrameRelated1 = UnkTextureData1[0];

            return oeDat;
        }
    }
}
