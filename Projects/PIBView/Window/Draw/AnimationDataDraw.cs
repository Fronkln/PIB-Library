using PIBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBView
{
    internal static class AnimationDataDraw
    {
        public static void Draw(Form1 form, TreeNodePibAnimationData animDatNode)
        {
            EmitterBaseAnimationData animDat = animDatNode.AnimationData;
            EmitterAnimationDataDE deAnimDat = animDatNode.AnimationData as EmitterAnimationDataDE;

            form.CreateHeader("Animation Data");
            form.CreateInput("Tick Unknown 1", animDat.TickUnknown1.ToString(), delegate (string val) { animDat.TickUnknown1 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Tick Unknown 2", animDat.TickUnknown2.ToString(), delegate (string val) { animDat.TickUnknown2 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Frame Unknown 1", animDat.FrameRelated1.ToString(), delegate (string val) { animDat.FrameRelated1 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Frame Unknown 2", animDat.FrameRelated2.ToString(), delegate (string val) { animDat.FrameRelated2 = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

            form.CreateSpace(10);

            for(int i = 0; i < animDat.TextureWidths.Length; i++)
            {
                int k = i;
                form.CreateInput($"Texture {i + 1} Width", animDat.TextureWidths[i].ToString(), delegate (string val) { animDat.TextureWidths[k] = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                form.CreateInput($"Texture {i + 1} Frames", animDat.TextureFrames[i].ToString(), delegate (string val) { animDat.TextureFrames[k] = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);

                if(deAnimDat != null)
                {
                    form.CreateInput($"Texture {i + 1} Unknown 1", deAnimDat.UnkTextureData1[i].ToString(), delegate (string val) { deAnimDat.UnkTextureData1[k] = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                    form.CreateInput($"Texture {i + 1} Unknown 2", deAnimDat.UnkTextureData2[i].ToString(), delegate (string val) { deAnimDat.UnkTextureData2[k] = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
                }
            }

            form.CreateSpace(10);

            form.CreateInput("Unknown X", animDat.UnknownX.ToString(), delegate (string val) { animDat.UnknownX = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
            form.CreateInput("Unknown Y", animDat.UnknownY.ToString(), delegate (string val) { animDat.UnknownY = Utils.InvariantParse(val); }, NumberBox.NumberMode.Float);
        }
    }
}
