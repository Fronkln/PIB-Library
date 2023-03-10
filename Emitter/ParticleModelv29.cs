using System.Drawing;
using System.Net;
using Yarhl.IO;

namespace PIBLib
{
    internal class ParticleModelv29 : ParticleSourcev29
    {
        internal override void ReadData(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            Data = reader.ReadBytes(160 * count);
        }
    }
}
