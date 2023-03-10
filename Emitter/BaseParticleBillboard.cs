using System.Drawing;
using System.Reflection.PortableExecutable;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseParticleBillboard : ParticleSource
    {
        internal override void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            Data = reader.ReadBytes(128 * count);
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Data);
        }

        internal override int GetDataCount()
        {
            return Data.Length / 128;
        }
    }
}
