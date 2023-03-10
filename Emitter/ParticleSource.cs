using Yarhl.IO;

namespace PIBLib
{
    //Base class for Model and Billboard
    public abstract class ParticleSource
    {
        public byte[] Data;

        internal virtual void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {

        }

        internal virtual void Write(DataWriter writer)
        {
            
        }

        internal virtual int GetDataCount()
        {
            return 0;
        }
    }
}
