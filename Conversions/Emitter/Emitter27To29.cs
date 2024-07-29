using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yarhl.IO;


namespace PIBLib.Conversions
{
    internal static class Emitter27To29
    {
        public unsafe static PibEmitterv29 Convert(PibEmitterv27 emitter27)
        {
            PibEmitterv29 emitter = new PibEmitterv29();
            emitter27.CopyFields(emitter);

            emitter.UnkRegion1 = emitter27.UnkRegion1;

            DEPibBaseMetaball metaball = new DEPibBaseMetaball();
            emitter27.Metaball.CopyFields(metaball);

            metaball.LtShininess = emitter27.Metaball.LtShininess * 2f;
            metaball.LtIoe = emitter27.Metaball.LtIoe * 2f;
            metaball.NmlRange *= 2;

            emitter.Metaball = metaball;

            EmitterAnimationDataDE deAnim = emitter.AnimationData.ToDE();
            emitter.AnimationData = deAnim;

            DEPibCommonUnkStructure2 unk2 = new DEPibCommonUnkStructure2();
            emitter27.CommonUnkStructure2.CopyFields(unk2);

            unk2.Unk5 = emitter27.CommonUnkStructure2.Unk2;
            unk2.Unk2 = 0;

            emitter.CommonUnkStructure2 = unk2;

            EmitterFlag1v27 flags = (EmitterFlag1v27)emitter27.Flags;

            int v29Flags = 0;
            emitter.Flags3 = 0; //all OE pibs converted to DE had this as zero

            if (emitter.Blend == 2 || emitter.Blend == 3)
                emitter.Blend = 1;


            //Convert new flags to v27 flags
            foreach (Enum flag in flags.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int v29Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag1v29), flagStr));

                    v29Flags |= v29Value;
                }
                catch
                {
                    Console.WriteLine("Flag not found v29-v27 " + flag.ToString());
                }
            }

            //Fix flags
            //1 << 8 existed on FKs0005(brawler pib) it didnt have color, removing it made it blue (probably better)
            emitter.UnknownFlags_0x8 = emitter.UnknownFlags_0x8.RemoveFlag(1 << 8);

            switch (emitter27.Type)
            {
                default:
                    throw new Exception("Dunno how to convert this geo vtx boss, " + emitter27.Type);

                case 0:
                    emitter.GeoVertex = 0;
                    emitter.UnknownData1 = new List<EmitterBaseDataChunk>();

                    foreach (EmitterDataChunkType0 type0 in emitter27.UnknownData1)
                    {
                        EmitterDataChunkType0DE type0DE = new EmitterDataChunkType0DE();
                        
                        for(int i = 0; i < type0.Data.Length; i++)
                            type0DE.Data[i] = type0.Data[i];

                        emitter.UnknownData1.Add(type0DE);
                    }

                    break;

                //no action needed
                case 1:
                    emitter.GeoVertex = 1;
                    break;

                //Convert type 2 to type 1
                case 2:
                    emitter.GeoVertex = 1;
                    emitter.UnknownData1 = new List<EmitterBaseDataChunk>();

                    foreach (EmitterDataChunkType2 type2 in emitter27.UnknownData1)
                        emitter.UnknownData1.Add(type2.ToType1());
                    break;

                case 3:
                    emitter.GeoVertex = 1;
                    emitter.UnknownData1 = new List<EmitterBaseDataChunk>();

                    foreach (EmitterDataChunkType3 type3 in emitter27.UnknownData1)
                        emitter.UnknownData1.Add(type3.ToType1());

                    break;

                //Convert type 5 to type 1
                case 5:
                    emitter.GeoVertex = 1;
                    emitter.UnknownData1 = new List<EmitterBaseDataChunk>();

                    foreach (EmitterDataChunkType5 type5 in emitter27.UnknownData1)
                        emitter.UnknownData1.Add(type5.ToType1());
                    break;
            }

            if(emitter.GetEmitterType() == EmitterType.Billboard)
            {
                List<BaseParticleBillboardData> newParticles = new List<BaseParticleBillboardData>();

                foreach (BaseParticleBillboardData dat in emitter27.Source.Particles)
                    newParticles.Add(dat.ToV29());

                emitter.Source.Particles = newParticles.Cast<ParticleIns>().ToList();
            }
            else
            {
                List<ParticleModelDatav29> newParticles = new List<ParticleModelDatav29>();

                foreach (BaseParticleModelData dat in emitter27.Source.Particles)
                    newParticles.Add(dat.ToV29());

                emitter.Source.Particles = newParticles.Cast<ParticleIns>().ToList();
            }

            emitter.Flags = v29Flags;

            return emitter;
        }
    }
}
