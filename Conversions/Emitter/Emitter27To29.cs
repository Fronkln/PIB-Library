using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;
using System.Buffers.Binary;


namespace PIBLib.Conversions
{
    internal static class Emitter27To29
    {
        public unsafe static PibEmitterv29 Convert(PibEmitterv27 emitter27)
        {
            PibEmitterv29 emitter = new PibEmitterv29();
            emitter27.CopyFields(emitter);

            //Extend & endian swap
            using (DataStream stream = DataStreamFactory.FromMemory())
            {
                //C# worst practices top 10
                DataReader reader = new DataReader(stream) { Endianness = EndiannessMode.BigEndian };
                DataWriter writer = new DataWriter(stream) { Endianness = EndiannessMode.BigEndian };

                writer.Write(emitter27.Unknown0x10);
                writer.Stream.Position = 0;
                writer.Endianness = EndiannessMode.LittleEndian;

                for(int i = 0; i < emitter27.Unknown0x10.Length / 4; i++)
                {
                    float flt = reader.ReadSingle();
                    stream.Position -= 4;
                    writer.Write(BitConverter.GetBytes(flt));
                }

                emitter27.Unknown0x10 = stream.ToArray();

                //Broke brawler pibs with its value of 3000, setting it to 0 fixed it. I don't know if there is a more appopriate value
                for (int i = 8; i < 12; i++)
                    emitter27.Unknown0x10[i] = 0;
            }

            //Fix flags
            //1 << 8 existed on FKs0005(brawler pib) it didnt have color, removing it made it blue (probably better)
            emitter.UnknownFlags_0x8 = emitter.UnknownFlags_0x8.RemoveFlag(1 << 8);

            List<byte> extended0x10 = new List<byte>(emitter27.Unknown0x10);
            extended0x10.InsertRange(0, new byte[4]);

            //surgery needed for a proper conversion
            List<byte> shrunkMainData = new List<byte>(emitter27.UnknownMainData);

            //Absolutely horribel, terribel indeeed....
            byte[] float05 = new byte[] { 0x3F, 0x0, 0, 0 };
            byte[] float1 = new byte[] { 0x3F, 0x80, 0, 0};

            shrunkMainData.InsertRange(16, new byte[8]);
            shrunkMainData.RemoveRange(52, 8);
            shrunkMainData.InsertRange(68, new byte[12]);


            shrunkMainData.RemoveRange(356, shrunkMainData.Count - 356);

            byte[] shrunkMainDataBuff = shrunkMainData.ToArray();

            fixed (byte* buffer = shrunkMainDataBuff)
            {
                using (DataStream stream = DataStreamFactory.FromMemory())
                {
                    DataWriter writer = new DataWriter(stream) { Endianness = EndiannessMode.BigEndian };
                    writer.Write(shrunkMainData.ToArray());

                    stream.Position = 44;
                    writer.Write(1f);
                    writer.Write(0.5f);
                    writer.Write(BinaryPrimitives.ReadSingleBigEndian(writer.Stream.ReadPart(4)) * 2);

                    writer.Stream.Position = 56;
                    writer.Write(new Vector2(1f, 1f));

                    writer.Stream.Position = 64;
                    writer.Write(0);
                    writer.Write(new Vector3(1, 1, 1));

                    writer.Stream.Position = 84;
                    writer.Write(new Vector3(1,1,1));
                    writer.Write(new Vector4(0.5f, 0.5f, 1f, 1f));

                    writer.Stream.Position = 112;
                    writer.Insert(20);
                    writer.Stream.Position = 112;
                    writer.Write(0.03f);
                    writer.Stream.Position = 260;
                   
                    byte[] area1 = writer.Stream.ReadPart(12);
                    writer.Stream.Position = 144;
                    writer.Write(area1);
                   
                    writer.Stream.Position = 156;
                    writer.Write(1f);
                    
                    writer.Write(new Vector4(1f, 1f, 1f, 1f));
                    writer.Write(new Vector4(1f, 1f, 1f, 1f));
                    writer.Write(1f);
                    
                    writer.Stream.Position = 304;
                    writer.Write(1f);


                    byte[] val1 = new byte[4];
                    byte[] val2 = new byte[4];

                    Array.Copy(emitter27.UnknownMainData, 244, val1, 0, 4);
                    Array.Copy(emitter27.UnknownMainData, 248, val2, 0, 4);

                    writer.Stream.Position = 320;
                    writer.Write(val1);
                    writer.Stream.Position = 336;
                    writer.Write(val2);

                    shrunkMainData = writer.Stream.ToArray().ToList();
                    shrunkMainData.RemoveRange(356, shrunkMainData.Count - 356);
                }
            }

            emitter.Unknown0x10 = extended0x10.ToArray();
            emitter.UnknownMainData = shrunkMainData.ToArray();

            //Endian swap
            for (int i = 0; i < emitter.UnknownMainData.Length; i += 4)
            {
                Array.Reverse(emitter.UnknownMainData, i, 4);
            }

            if (emitter27.Source is BaseParticleBillboard)
            {
                ParticleBillboardv29 convertedData = new ParticleBillboardv29();
                BaseParticleBillboard billboard = (emitter27.Source as BaseParticleBillboard);

                foreach (BaseParticleBillboardData dat in billboard.Data)
                    convertedData.Data.Add(dat.ToV29());

                emitter.Source = convertedData;
            }
            else
            {
                ParticleModelv29 convertedData = new ParticleModelv29();
                BaseParticleModel model = (emitter27.Source as BaseParticleModel);

                foreach (BaseParticleModelData dat in model.Data)
                    convertedData.Data.Add(dat.ToV29());

                emitter.Source = convertedData;
            }

            switch (emitter.Type)
            {
                //Convert type 2 to type 1
                case 2:
                    emitter.Type = 1;
                    emitter.UnknownData1 = new List<EmitterBaseDataChunk>();

                    foreach (EmitterDataChunkType2 type2 in emitter27.UnknownData1)
                        emitter.UnknownData1.Add(type2.ToType1());
                    break;

                //Convert type 5 to type 1
                case 5:
                    emitter.Type = 1;
                    emitter.UnknownData1 = new List<EmitterBaseDataChunk>();

                    foreach (EmitterDataChunkType5 type5 in emitter27.UnknownData1)
                        emitter.UnknownData1.Add(type5.ToType1());
                    break;
            }

            return emitter;
        }
    }
}
