using System;
using System.Linq;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter29To27
    {
        public unsafe static PibEmitterv27 Convert(PibEmitterv29 emitter29)
        {
            PibEmitterv27 emitter = new PibEmitterv27();
            emitter29.CopyFields(emitter);

            emitter.Type = 1;

            if (emitter.Source is ParticleBillboardv29)
            {
                BaseParticleBillboard billboard = new BaseParticleBillboard();

                foreach (ParticleBillboardDatav29 dat in emitter29.Source.Particles)
                {
                    BaseParticleBillboardData datay = new BaseParticleBillboardData();
                    dat.CopyFields(datay);
                    datay.Scale.z /= 2.93f;
                    billboard.Particles.Add(datay);
                }

                emitter.Source = billboard;
            }
            else
            {
                BaseParticleModel model = new BaseParticleModel();

                foreach (ParticleModelDatav29 dat in emitter29.Source.Particles)
                {
                    model.Particles.Add(dat.ToV19());
                }

                emitter.Source = model;
            }


            PibBaseMetaball metaball = new PibBaseMetaball();
            emitter.Metaball.CopyFields(metaball);

            DEPibBaseMetaball deMetaball = emitter29.Metaball as DEPibBaseMetaball;

            metaball.LtShininess = deMetaball.LtShininess / 2f;
            metaball.LtIoe = deMetaball.LtIoe / 2f;

            emitter.Metaball = metaball;

            PibBaseCommonUnkStructure2 unkStr2 = new PibBaseCommonUnkStructure2();
            emitter.CommonUnkStructure2.CopyFields(unkStr2);

            unkStr2.Unk2 = (emitter29.CommonUnkStructure2 as DEPibCommonUnkStructure2).Unk5;
            emitter.CommonUnkStructure2 = unkStr2;
            emitter.OOEUnkStructure4.Unknown9 = emitter29.CommonUnkStructure2.Unk3;
            emitter.OOEUnkStructure4.Unknown5 = emitter29.Gravity;


            EmitterFlag1v29 flags = (EmitterFlag1v29)emitter29.Flags;
            EmitterFlag2v52 flags2 = (EmitterFlag2v52)emitter29.Flags2;
            EmitterFlag3v29 flags3 = (EmitterFlag3v29)emitter29.Flags3;
            int v27Flags = 0;
            int v27Flags2 = 0;
            int v27Flags3 = 0;

            //Convert new flags to v27 flags
            foreach (Enum flag in flags.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int v27Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag1v27), flagStr));

                    v27Flags |= v27Value;
                }
                catch
                {
                    Console.WriteLine("Flag not found v29-v27 "  + flag.ToString());
                }
            }

            v27Flags3 |= 1 << 3;
            v27Flags3 |= 1 << 7;

            if (flags2.HasFlag(EmitterFlag2v52.eFLG_FIX_AXIS))
            {
                v27Flags3 |= (int)EmitterFlag3v27.FIX_AXIS;

                //HAa0011
                if(emitter.GetEmitterType() != EmitterType.Billboard)
                    v27Flags3 |= (int)EmitterFlag3v27.UNK_V21_FLAG9;
            }

            emitter.Flags = v27Flags;
            emitter.Flags2 = v27Flags2;
            emitter.Flags3 = v27Flags3;

            if (emitter29.AnimationData.TextureFrames.Any(x => x > 1))
                emitter.OEUnknown9 = 1;

            emitter.AnimationData = (emitter29.AnimationData as EmitterAnimationDataDE).ToOE();
            emitter.AnimationData.FrameRelated2 = 1;
            emitter.OOEUnkStructure3.Unk1 = (emitter29.AnimationData as EmitterAnimationDataDE).FrameRelated2;

            //Geo VTX Conversion
            EmitterBaseDataChunk[] chunks = emitter.UnknownData1.Cast<EmitterBaseDataChunk>().ToArray();

            //shrink geovertex 0 to 44 bytes
            if (emitter29.GeoVertex == 0)
            {
                EmitterBaseDataChunk[] newChunks = new EmitterBaseDataChunk[chunks.Length];
                for (int i = 0; i < chunks.Length; i++)
                {
                    EmitterDataChunkType1 chunk = new EmitterDataChunkType1();
                    Array.Copy(chunks[i].Data, chunk.Data, chunk.Data.Length);
                    newChunks[i] = chunk;
                }

                chunks = newChunks;
                emitter.UnknownData1 = newChunks.ToList();
            }


            emitter.Metaball.NmlRange /= 2;
            emitter.Metaball.NmlRange *= 0.01f;

            //Fix metaball flags (hacky)
            if (flags.HasFlag(EmitterFlag1v29.eFLG_METABALL))
            {
                emitter.Blend = 1;
                emitter.MetaballBlend = 2;
                emitter.Flags |= (int)EmitterFlag1v27.eFLG_METABALL_R;
                emitter.Flags |= (int)EmitterFlag1v27.eFLG_UNK_V27_FLAG2;
                emitter.Flags |= (int)EmitterFlag1v27.eFLG_UNK_V21_FLAG;

                emitter.Flags3 = 9;
            }


            //Fix metaball geo vtx (hacky)
            if(flags.HasFlag(EmitterFlag1v29.eFLG_METABALL))
            {
                EmitterDataChunkType1 chunk1 = emitter.UnknownData1[0] as EmitterDataChunkType1;
                EmitterDataChunkType1 chunk2 = emitter.UnknownData1[1] as EmitterDataChunkType1;
                EmitterDataChunkType1 chunk3 = emitter.UnknownData1[2] as EmitterDataChunkType1;
                EmitterDataChunkType1 chunk4 = emitter.UnknownData1[3] as EmitterDataChunkType1;

                for (int i = 6; i < 11; i++)
                    chunk1.Data[i] = 1;

                chunk2.Data[4] = 1;
                chunk2.Data[6] = 1;
                chunk2.Data[7] = 0;
                chunk2.Data[8] = 1;
                chunk2.Data[9] = 0;
                chunk2.Data[10] = 1;

                for (int i = 3; i < 11; i++)
                    chunk1.Data[i] = 1;
            }

            return emitter;
        }
    }
}
