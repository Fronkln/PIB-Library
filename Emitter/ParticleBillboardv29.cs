using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    internal class ParticleBillboardv29 : ParticleSourcev29
    {
        internal override void ReadData(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            Data = reader.ReadBytes(144 * count);
        }

        internal override int GetDataCount()
        {
            return Data.Length / 144;
        }
    }
}
