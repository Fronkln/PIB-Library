using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    public static class Emitter52To45
    {
        public static PibEmitterv45 Convert(PibEmitterv52 emitter52)
        {
            PibEmitterv45 emitter = new PibEmitterv45();
            emitter52.CopyFields(emitter);

            DEPibBaseUnkStructure3 unkStructure3 = new DEPibBaseUnkStructure3();
            emitter52.UnkStructure3.CopyFields(unkStructure3);

            DEPibv45UnkStructure1 unkStr1 = new DEPibv45UnkStructure1();
            emitter52.v45Unk1.CopyFields(unkStr1);

            emitter.v45Unk1 = unkStr1;

            /*
            DEPibv45UnkStructure3 v52Struct3 = emitter52.UnkStructure3 as DEPibv45UnkStructure3;

            unkStructure3.Unk6 = v52Struct3.Unk10;
            unkStructure3.Unk7 = v52Struct3.Unk11;
            unkStructure3.Unk8 = v52Struct3.Unk12;

            emitter.UnkStructure3 = unkStructure3;
            */

            emitter.GeoVertex = 1;

            //Textures have dds prefix
            for (int i = 0; i < emitter.Textures.Count; i++)
                if (!emitter.Textures[i].EndsWith(".dds"))
                    emitter.Textures[i] += ".dds";

            EmitterFlag1v52 flags = (EmitterFlag1v52)emitter52.Flags;
            int flags2 = (int)emitter52.Flags2;
            EmitterFlag3v52 flags3 = (EmitterFlag3v52)emitter52.Flags3;
            uint de1Flags = 0;
            uint de1Flags3 = 0;

            //Flags that dont exist in JE
            flags &= ~EmitterFlag1v52.eFLG_METABALL_A_THRESHOLD_01;
            flags &= ~EmitterFlag1v52.eFLG_METABALL_WORK_X4;
            flags &= ~EmitterFlag1v52.eFLG_MESH;

            //crashes JE
            flags &= ~EmitterFlag1v52.eFLG_MESH_VAT;

            flags3 &= ~EmitterFlag3v52.eFLG_TEX_A1_WRAP_CLAMP;
            flags3 &= ~EmitterFlag3v52.eFLG_TEX_A_PATTERN_ONESHOT;
            flags3 &= ~EmitterFlag3v52.eFLG_TEX_B_NORMAL_LIGHT;
            flags3 &= ~EmitterFlag3v52.eFLG_LIGHTING_EMISSIVE_CURVE_RGB;
            flags3 &= ~EmitterFlag3v52.eFLG_BILLBOARD_AXIS_Y;
            flags3 &= ~EmitterFlag3v52.eFLG_STAGE_REFLECTION;
            flags3 &= ~ EmitterFlag3v52.eFLG_LIGHTING_EMISSIVE_CURVE_A;
            flags3 &= ~EmitterFlag3v52.eFLG_TEX_B_NORMAL_REFRACTION;

            /*
            if (flags3.HasFlag(EmitterFlag3v52.eFLG_TEX_B_NORMAL_LIGHT))
            {
                flags3 &= ~EmitterFlag3v52.eFLG_TEX_B_NORMAL_LIGHT;
            }
            */

            //Convert new flags to v45 flags
            foreach (Enum flag in flags.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    uint de1Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag1v45), flagStr));

                    de1Flags |= de1Value;
                }
                catch
                {

                }
            }

            //18.11.2024 qsr0265-qsc0265 weirdness?
            if(flags2.HasFlag((int)EmitterFlag2v52.eFLG_TRACK_CROSS))
            {
                flags2 &= (int)~EmitterFlag2v52.eFLG_TRACK_CROSS;
                flags2 |= (1 << 28);
            }

            if (flags2.HasFlag((int)EmitterFlag2v52.eFLG_TRACK_OVERWRITE))
            {
                flags2 &= (int)~EmitterFlag2v52.eFLG_TRACK_OVERWRITE;
                flags2 |= (1 << 31);
            }

            //18.11.2024 qsr0265-qsc0265 weirdness?
            bool shouldAdjustTextures = emitter.UnkNumbers_TextureTable_V42.Any(x => x <= 2);

            if (shouldAdjustTextures)
                for (int i = 0; i < emitter.UnkNumbers_TextureTable_V42.Length; i++)
                    emitter.UnkNumbers_TextureTable_V42[i] += 2;

            //Convert new flags3 to v45 flags3
            foreach (Enum flag in flags3.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    uint de2Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag3v43), flagStr));
                    de1Flags3 |= de2Value;
                }
                catch
                {

                }
            }

            //09.07.2024 Yyj0056
            if (((EmitterFlag3v52)emitter52.Flags3).HasFlag(EmitterFlag3v52.eFLG_TEX_B_NORMAL_REFRACTION))
            {
                de1Flags3 |= (uint)EmitterFlag3v43.eFLG_TEX_REFLECTION;
            }


            emitter.Flags = (int)de1Flags;
            emitter.Flags2 = (int)flags2;
            emitter.Flags3 = (int)de1Flags3;

            emitter.PropertyAnimationCurve.RemoveAt(2);
            emitter.PropertyAnimationCurve.Add(new PibEmitterAnimationCurveGeneric() { Values = new float[emitter.PropertyAnimationCurve[0].GetDataSize() / 4] });

            //DDS header flags
            emitter.DDSHeader.TextureFormat &= ~4;

            //DE 1.0: UV size on Geo VTX Chunk
            if (emitter.UnknownData1.Count == 4)
            {
                emitter.UnknownData1[0].Data[7] = emitter.UV.UVSize[1].x;
                emitter.UnknownData1[0].Data[8] = emitter.UV.UVSize[1].y;

                emitter.UnknownData1[0].Data[9] = emitter.UV.UVSize[2].x;
                emitter.UnknownData1[0].Data[10] = emitter.UV.UVSize[2].y;


                emitter.UnknownData1[2].Data[3] = emitter.UV.UVSize[0].x;
                emitter.UnknownData1[2].Data[4] = emitter.UV.UVSize[0].y;

                emitter.UnknownData1[2].Data[5] = emitter.UV.UVSize[1].x;
                emitter.UnknownData1[2].Data[6] = emitter.UV.UVSize[1].y;

                emitter.UnknownData1[2].Data[7] = emitter.UV.UVSize[2].x;
                emitter.UnknownData1[2].Data[8] = emitter.UV.UVSize[2].y;

                emitter.UnknownData1[2].Data[9] = emitter.UV.UVSize[2].x;
                emitter.UnknownData1[2].Data[10] = emitter.UV.UVSize[2].y;

                emitter.UnknownData1[1].Data[4] = emitter.UV.UVSize[0].y;
                emitter.UnknownData1[1].Data[6] = emitter.UV.UVSize[1].y;

                emitter.UnknownData1[1].Data[8] = emitter.UV.UVSize[2].x;
                emitter.UnknownData1[1].Data[10] = emitter.UV.UVSize[2].y;

                emitter.UnknownData1[3].Data[5] = emitter.UV.UVSize[1].x;

                emitter.UnknownData1[3].Data[3] = 1; //??? yjh0038
                emitter.UnknownData1[3].Data[8] = emitter.UV.UVSize[2].x;
                emitter.UnknownData1[3].Data[9] = 0; //??? yjh0038
                emitter.UnknownData1[3].Data[10] = emitter.UV.UVSize[2].y;
            }

            return emitter;
        }
    }
}
