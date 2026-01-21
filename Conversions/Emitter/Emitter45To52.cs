using System;
using System.Reflection;
using System.Collections.Generic;
using Yarhl.IO;
using System.Buffers.Binary;
using System.Linq;

namespace PIBLib.Conversions
{
    public static class Emitter45To52
    {
        public static PibEmitterv52 Convert(PibEmitterv45 emitterv45)
        {
            PibEmitterv52 emitter = new PibEmitterv52();
            emitterv45.CopyFields(emitter);

            DEPibv45UnkStructure3 unkStructure3 = new DEPibv45UnkStructure3();
            emitterv45.UnkStructure3.CopyFields(unkStructure3);

            emitter.v45Unk1 = emitterv45.v45Unk1;
            emitter.UnkStructure3 = unkStructure3;

            emitter.Flags = (ulong)emitterv45.Flags;
            emitter.Flags2 = (ulong)emitterv45.Flags2;
            emitter.Flags3 = (ulong)emitterv45.Flags3;

            //Important: else it causes crashing
            //Tested on various pibs between JE and YLAD
            emitter.GeoVertex = 0;

            //Textures no longer have dds prefix
            for (int i = 0; i < emitter.Textures.Count; i++)
                if (emitter.Textures[i].EndsWith(".dds"))
                    emitter.Textures[i] = emitter.Textures[i].Replace(".dds", "");

            // emitter.TextureUnks = new byte[36 * emitter.Textures.Count];

            /*
            for (int i = 0; i < emitter.Textures.Count; i++)
                emitter.TextureImports.Add(new TextureImportInfo());
            */

            EmitterType type = emitterv45.GetEmitterType();

            EmitterFlag1v45 flags = (EmitterFlag1v45)emitter.Flags;
            EmitterFlag2v45 flags2 = (EmitterFlag2v45)emitter.Flags2;
            EmitterFlag3v43 flags3 = (EmitterFlag3v43)emitter.Flags3;
            long de2Flags = 0;
            long de2Flags2 = 0;
            long de2Flags3 = 0;

            //eFLG_UNK_V43_FLAG which doesnt exist in DE2 at a first glance?
            flags &= ~EmitterFlag1v45.eFLG_UNK_V29_FLAG;

            //Doesnt exist in DE2 at a first glance?
            flags2 &= ~EmitterFlag2v45.Flag11;

            //All metaballs i've looked into were given this flag when converted to DE
            if (emitter.IsMetaball())
                de2Flags |= (long)EmitterFlag1v52.eFLG_METABALL_A_THRESHOLD_01;

            //Convert old flags to DE 2.0 flags
            foreach (Enum flag in flags.GetFlags())
            {
                string flagStr = flag.ToString();
                long de2Value = System.Convert.ToInt64(Enum.Parse(typeof(EmitterFlag1v52), flagStr));

                de2Flags |= de2Value;
            }


            //Convert old flags2 to DE 2.0 flags
            foreach (Enum flag in flags2.GetFlags())
            {
                string flagStr = flag.ToString();
                long de2Value = System.Convert.ToInt64(Enum.Parse(typeof(EmitterFlag2v52), flagStr));

                de2Flags2 |= (long)de2Value;
            }

            //Convert old flags3 to DE 2.0 flags3
            foreach (Enum flag in flags3.GetFlags())
            {
                string flagStr = flag.ToString();
                try
                {
                    long de2Value = System.Convert.ToInt64(Enum.Parse(typeof(EmitterFlag3v52), flagStr));
                    de2Flags3 |= (long)de2Value;
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("V52 flag 3 not found " + flag.ToString());
                }
            }

            de2Flags3 |= (long)EmitterFlag3v52.eFLG_TEX_A1_WRAP_CLAMP;

            emitter.Flags = (ulong)de2Flags;
            emitter.Flags2 = (ulong)de2Flags2;
            emitter.Flags3 = (ulong)de2Flags3;

            //DE 1.0: UV size on Geo VTX Chunk
            if (emitter.UnknownData1.Count == 4)
            {
                //Metaball GeoVTX is different
                if(!emitter.IsMetaball())
                {
                    emitter.UV.UVSize[0].x = emitter.UnknownData1[2].Data[3];
                    emitter.UV.UVSize[0].y = emitter.UnknownData1[1].Data[4];

                    emitter.UV.UVSize[1].x = emitter.UnknownData1[3].Data[5];
                    emitter.UV.UVSize[1].y = emitter.UnknownData1[2].Data[6];
                }
                else
                {
                    emitter.UV.UVSize[0].x = emitter.UnknownData1[2].Data[9];
                    emitter.UV.UVSize[0].y = emitter.UnknownData1[2].Data[10];
                }

                /*
                emitter.UV.UVSize[0].x = emitter.UnknownData1[2].Data[3];
                emitter.UV.UVSize[0].y = emitter.UnknownData1[2].Data[4];

                emitter.UV.UVSize[1].x = emitter.UnknownData1[2].Data[5];
                emitter.UV.UVSize[1].y = emitter.UnknownData1[2].Data[6];

                emitter.UV.UVSize[2].x = emitter.UnknownData1[2].Data[7];
                emitter.UV.UVSize[2].y = emitter.UnknownData1[2].Data[8];
                */

                //Not present data, defaults to 1,1
                //21.12.2015: Are you sure about that
                emitter.UV.UVSize[3].x = 1;
                emitter.UV.UVSize[3].y = 1;
            }

            /*
            //and now fix them to defaults appopriately if we messed it up)
            if (emitter.UV.UVSize[0].x == 0 || emitter.UV.UVSize[0].y == 0)
            {
                emitter.UV.UVSize[0].x = 1;
                emitter.UV.UVSize[0].y = 1;
            }

            if (emitter.UV.UVSize[1].x == 0 || emitter.UV.UVSize[1].y == 0)
            {
                emitter.UV.UVSize[1].x = 1;
                emitter.UV.UVSize[1].y = 1;
            }

            if (emitter.UV.UVSize[2].x == 0 || emitter.UV.UVSize[2].y == 0)
            {
                emitter.UV.UVSize[2].x = 1;
                emitter.UV.UVSize[2].y = 1;
            }
            */


            //Geo VTX Conversion
            EmitterBaseDataChunk[] chunks = emitter.UnknownData1.Cast<EmitterBaseDataChunk>().ToArray();
            
            //shrink geovertex 0 to 44 bytes
            if(emitter.GeoVertex == 0)
            {
                EmitterBaseDataChunk[] newChunks = new EmitterBaseDataChunk[chunks.Length];
                for(int i = 0; i < chunks.Length; i++)
                {
                    EmitterDataChunkType1 chunk = new EmitterDataChunkType1();
                    Array.Copy(chunks[i].Data, chunk.Data, chunk.Data.Length);
                    newChunks[i] = chunk;
                }

                chunks = newChunks;
                emitter.UnknownData1 = newChunks.ToList();
            }
            
            if(!emitter.IsMetaball())
            {
                for (int i = 2; i < chunks[0].Data.Length; i++)
                    chunks[0].Data[i] = 0;

                for (int i = 2; i < chunks[3].Data.Length; i++)
                    chunks[3].Data[i] = 0;

                chunks[3].Data[9] = 1;

                for (int i = 2; i < chunks[1].Data.Length; i++)
                    chunks[1].Data[i] = 0;

                chunks[1].Data[chunks[1].Data.Length - 1] = 1;

                chunks[2].Data[9] = 1;
                chunks[2].Data[10] = 1;

                for (int i = 2; i < 9; i++)
                    chunks[2].Data[i] = 0;
            }

            /*
            //Having troubles converting this properly. Need to compare more pibs
            if((emitter.Flags & (ulong)EmitterFlag1v52.eFLG_METABALL) != 0)
            {
                chunks[0].Data[1] = -chunks[0].Data[1];

                for (int i = 7; i < chunks[0].Data.Length; i++)
                    chunks[0].Data[i] = 0;

                chunks[0].Data[10] = 1;

                for (int i = 2; i < chunks[3].Data.Length; i++)
                    chunks[3].Data[i] = 0;

                chunks[3].Data[9] = 1;
            }
            else
            {
                for (int i = 2; i < chunks[0].Data.Length; i++)
                    chunks[0].Data[i] = 0;

                for (int i = 2; i < chunks[3].Data.Length; i++)
                    chunks[3].Data[i] = 0;

                chunks[3].Data[9] = 1;
            }

            for (int i = 2; i < chunks[1].Data.Length; i++)
                chunks[1].Data[i] = 0;

            chunks[1].Data[chunks[1].Data.Length - 1] = 1;

            chunks[2].Data[9] = 1;
            chunks[2].Data[10] = 1;

            for (int i = 2; i < 9; i++)
                chunks[2].Data[i] = 0;
            */

            //v52: Shift happened at the 3rd curve, Color Curve is now the 7th one instead of 6th
            //8th curve seems to be new. used to be empty data. Compare: JE and YLAD AAa0000.pib AAa0001.pib
            emitter.PropertyAnimationCurve.Insert(2, emitter.PropertyAnimationCurve[2]);
            emitter.PropertyAnimationCurve.RemoveAt(emitter.PropertyAnimationCurve.Count - 1);

            return emitter;
        }
    }
}
