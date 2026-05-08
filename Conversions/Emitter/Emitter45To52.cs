using System;
using System.Reflection;
using System.Collections.Generic;
using Yarhl.IO;
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
            EmitterFlag2v43 flags2 = (EmitterFlag2v43)emitter.Flags2;
            EmitterFlag3v43 flags3 = (EmitterFlag3v43)emitter.Flags3;
            long de2Flags = 0;
            long de2Flags2 = 0;
            long de2Flags3 = 0;

            //eFLG_UNK_V43_FLAG and 2 which doesnt exist in DE2 at a first glance?
            flags &= ~EmitterFlag1v45.eFLG_UNK_V29_FLAG;
            flags &= ~EmitterFlag1v45.eFLG_UNK_V29_FLAG2;

            //Doesnt exist in DE2 at a first glance?
            flags2 &= ~EmitterFlag2v43.Flag11;

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


            EmitterBaseDataChunk[] chunksPre = emitter.UnknownData1.Cast<EmitterBaseDataChunk>().ToArray();

            //shrink geovertex 0 to 44 bytes
            if (emitter.GeoVertex == 0)
            {
                EmitterBaseDataChunk[] newChunks = new EmitterBaseDataChunk[chunksPre.Length];

                for (int i = 0; i < chunksPre.Length; i++)
                {
                    EmitterDataChunkType1 oldChunk = chunksPre[i] as EmitterDataChunkType0DE;
                    EmitterDataChunkType1 chunk = new EmitterDataChunkType1();
                    chunk.Position = oldChunk.Position;
                    chunk.UV01 = oldChunk.UV01;
                    chunk.UV23 = oldChunk.UV23;

                    newChunks[i] = chunk;
                }

                chunksPre = newChunks;
                emitter.UnknownData1 = newChunks.ToList();
            }


            EmitterDataChunkType1[] chunks = emitter.UnknownData1.Cast<EmitterDataChunkType1>().ToArray();

            if (!emitter.IsMetaball())
            {
                //UV1
                emitter.UV.UVSize[0].x = chunks[2].UV01.x;
                emitter.UV.UVSize[0].y = chunks[2].UV01.y;

                //UV2
                emitter.UV.UVSize[1].x = chunks[2].UV01.z;
                emitter.UV.UVSize[1].y = chunks[2].UV01.w;

                //UV3
                emitter.UV.UVSize[2].x = chunks[2].UV23.x;
                emitter.UV.UVSize[2].y = chunks[2].UV23.y;

                //UV01s are zeroed out after moving it to the DE 2.0 UV struct
                for (int i = 0; i < chunks.Length; i++)
                    chunks[i].UV01 = new Vector4();
            }
            else
            {
                //UV1
                emitter.UV.UVSize[0].x = chunks[2].UV01.z;
                emitter.UV.UVSize[0].y = chunks[2].UV01.w;

                chunks[2].UV01.z = 1;
                chunks[2].UV01.w = 1;
            }

            //Both in metaball and normal pibs
            chunks[0].UV23 = new Vector4(0, 0, 0, 0);
            chunks[1].UV23 = new Vector4(0, 0, 0, 1);
            chunks[2].UV23 = new Vector4(0, 0, 1, 1);
            chunks[3].UV23 = new Vector4(0, 0, 1, 0);

            //v52: Shift happened at the 3rd curve, Color Curve is now the 7th one instead of 6th
            //8th curve seems to be new. used to be empty data. Compare: JE and YLAD AAa0000.pib AAa0001.pib
            emitter.PropertyAnimationCurve.Insert(2, emitter.PropertyAnimationCurve[2]);
            emitter.PropertyAnimationCurve.RemoveAt(emitter.PropertyAnimationCurve.Count - 1);

            //Important: else it causes crashing
            //Tested on various pibs between JE and YLAD
            emitter.GeoVertex = 0;
   
            return emitter;
        }
    }
}
