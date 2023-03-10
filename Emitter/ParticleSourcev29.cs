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
        public byte[] UnkFloatData;

        internal override void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            ReadData(reader, emitter, flags, count, version);

            if (emitter.Type == 0)
                UnkFloatData = reader.ReadBytes(144);
        }

        internal virtual void ReadData(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {

        }
    }
}
