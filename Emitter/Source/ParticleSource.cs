using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib
{
    //Base class for Model and Billboard
    public abstract class ParticleSource
    {
        public List<ParticleIns> Particles = new List<ParticleIns>();

        internal virtual void Read(DataReader reader, BasePibEmitter emitter, int flags, int count, uint version)
        {

        }

        internal virtual void Write(DataWriter writer, PibVersion version)
        {

        }

        public virtual int GetDataCount()
        {
            return 0;
        }

        public virtual EmitterType GetDataType()
        {
            return 0;
        }

        public virtual List<object> GetData()
        {
            return null;
        }
    }
}
