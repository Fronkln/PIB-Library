using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Emitter52To58
    {
        public static PibEmitterv52 Convert(PibEmitterv52 emitter52)
        {
            PibEmitterv58 emitter = new PibEmitterv58();
            emitter52.CopyFields(emitter);

            foreach (string texture in emitter52.Textures)
            {
                emitter.TextureImports.Add(new TextureImportInfo());
            }

            int resID = 1;
            foreach (string texture in emitter52.ExtraTextures)
            {
                emitter.TextureImports[emitter.TextureImports.Count - 1].Resources.Add(new TextureImportResource() { ID = resID, Name = texture });
                resID++;
            }

            EmitterFlag2v52 flags2 = (EmitterFlag2v52)emitter.Flags2;
            long de2Flags2 = 0;

            //Convert old flags2 to new flags2
            foreach (Enum flag in flags2.GetFlags())
            {
                string flagStr = flag.ToString();
                int de2Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag2v58), flagStr));

                de2Flags2 |= (long)de2Value;
            }

            emitter.Flags2 = (ulong)de2Flags2;

            //4 new animation chunks
            PibEmitterAnimationCurveRGBA32F newChunk1 = new PibEmitterAnimationCurveRGBA32F();
            newChunk1.Values = new RGBA32F[emitter.PropertyAnimationCurve[0].GetDataSize() / 16];

            RGBA32F defaultVal = new RGBA32F(0, 0, 0, 1);

            for(int i = 0; i < newChunk1.Values.Length; i++)
            {
                newChunk1.Values[i] = defaultVal;
            }
            
            //identical default data fys1016 YLAD LADIW
            emitter.PropertyAnimationCurve.Add(newChunk1);
            emitter.PropertyAnimationCurve.Add(newChunk1);
            emitter.PropertyAnimationCurve.Add(newChunk1);
            emitter.PropertyAnimationCurve.Add(newChunk1);

            return emitter;
        }
    }
}
