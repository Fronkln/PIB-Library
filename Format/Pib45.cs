using PIBLib.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    //Changed:
    //Unknown Vector got shifted back by 8 bytes
    //Emitter main data (564 > 596 bytes)
    public class Pib45 : Pib43
    {
        internal override void Read(DataReader reader)
        {
            ReadCorePibData(reader);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    UnkFloats[i, j] = reader.ReadSingle();

            Unknown_0x23C = reader.ReadBytes(20);

            BaseMatrix = reader.ReadMatrix4x4();
            Scale = reader.ReadVector3();

            Unknown_0x29C = reader.ReadBytes(16);
            Unknown0x2B4 = reader.ReadVector2();
            reader.ReadBytes(12);

            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv45 emitter = new PibEmitterv45();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            writer.Write(ParticleID);
            writer.Write(Emitters.Count);
            writer.Write(Duration);
            writer.Write(Unknown1);

            writer.Write(Speed);
            writer.Write(Unknown2);
            writer.Write(Unknown3);

            writer.Write(Unknown4);
            writer.Write(Unknown5);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 26; j++)
                    writer.Write(UnkFloats[i, j]);

            writer.Write(Unknown_0x23C);

            writer.Write(BaseMatrix);
            writer.Write(Scale);

            writer.Write(Unknown_0x29C);
            writer.Write(Unknown0x2B4);
            writer.WriteTimes(0, 8);

            foreach (BasePibEmitter emitter in Emitters)
                emitter.Write(writer);
        }

        public Pib43 ToV43()
        {
            return Pib45to43.Convert(this);
        }
    }
}
