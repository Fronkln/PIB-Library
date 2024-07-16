using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    internal class ParticleSourcev29 : ParticleSource
    {
        public byte[] UnkFloatData = null;

        internal override void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            ReadData(reader, emitter, flags, count, version);
        }

        internal virtual void ReadData(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {

        }

        internal override void Write(DataWriter writer, PibVersion version)
        {
            WriteData(writer, version);

            if (UnkFloatData != null)
                writer.Write(UnkFloatData);
        }

        protected virtual void WriteData(DataWriter writer, PibVersion version)
        {

        }
    }
}
