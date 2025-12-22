using PIBLib.Conversions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib29 : BaseDEPib
    {
        internal override void Read(DataReader reader)
        {
            base.Read(reader);
            ReadEmitters(reader, (int)EmitterCount);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv29 emitter = new PibEmitterv29();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }
        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            foreach (BasePibEmitter emitter in Emitters)
                emitter.Write(writer, Version);
        }
      
        public Pib27 ToV27()
        {
            return Pib29To27.Convert(this);
        }

        public Pib43 ToV43()
        {
            return Pib29To43.Convert(this);
        }
    }
}
