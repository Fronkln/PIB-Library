using System.IO;
using Yarhl.IO;

namespace PIBLib
{
    public class BaseParticleModel : ParticleSource
    {
        internal override void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {
            Data = reader.ReadBytes(144 * count);
        }

        internal override void Write(DataWriter writer)
        {
            writer.Write(Data);
        }

        internal override int GetDataCount()
        {
            return Data.Length / 144;
        }
    }
}
