using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class PibEmitterv19 : BasePibEmitter
    {
        internal override void Read(DataReader reader, PibVersion version)
        {
            Flags = reader.ReadUInt32();

            Unknown_0x4 = reader.ReadBytes(8);

            UnknownCount_0xC = reader.ReadByte();
            Type = reader.ReadByte();
            reader.ReadBytes(2);

            Unknown0x10 = reader.ReadBytes(36);

            int unknownCount1 = reader.ReadInt32();

            UnknownMainData = reader.ReadBytes(328);

            int data1Size = reader.ReadInt32(); //Includes DDS header

            //Endian swapped section
            reader.Endianness = EndiannessMode.LittleEndian;
            DDSHeader = reader.ReadBytes(128);


            int floatCount = (data1Size - 128) / 4;

            UnknownSection1 = new float[floatCount];

            for (int i = 0; i < floatCount; i++)
                UnknownSection1[i] = reader.ReadSingle();

            reader.Endianness = EndiannessMode.BigEndian;
            //End of endian swapped section

            EmitterType emitterType = GetEmitterType();
            Source = emitterType == EmitterType.Model ? new BaseParticleModel() : new BaseParticleBillboard();

            byte textureCount = reader.ReadByte();
            reader.Stream.Position -= 1;

            for (int i = 0; i < textureCount; i++)
            {
                reader.ReadByte();
                Textures.Add(reader.ReadString(31).Split(new[] { '\0' }, 2)[0]);
            }

            reader.Stream.Position += 3;

            int unknownCount2 = reader.ReadInt32();

            ReadUnknownData1(reader, Type, unknownCount1);
            Source.Read(reader, this, (int)Flags, unknownCount2, (uint)version);
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Flags);
            writer.Write(Unknown_0x4);
            
            writer.Write(UnknownCount_0xC);
            writer.Write(Type);
            writer.WriteTimes(0, 2);

            writer.Write(Unknown0x10);

            writer.Write(GetUnknownDataCount());
            writer.Write(UnknownMainData);
            writer.Write(896);

            writer.Endianness = EndiannessMode.LittleEndian;
            writer.Write(DDSHeader);

            foreach (float f in UnknownSection1)
                writer.Write(f);

            writer.Endianness = EndiannessMode.BigEndian;

            writer.Write((byte)Textures.Count);

            for(int i = 0; i < Textures.Count; i++)
            {
                if (i > 0)
                    writer.Write((byte)0);

                writer.Write(Textures[i].ToLength(32), false);
            }
            writer.WriteTimes(0, 2);
            writer.Write(Source.GetDataCount());

            writer.Write(UnknownData1);
            Source.Write(writer);
        }
    }
}
