using PIBLib.Conversions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib
{
    public class Pib27 : Pib25
    {
        protected override void ReadEmitters(DataReader reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PibEmitterv27 emitter = new PibEmitterv27();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }
        }

        internal override void Write(DataWriter writer)
        {
            WriteHeader(writer);

            foreach (var emitter in Emitters)
                emitter.Write(writer, Version);
        }

        public Pib25 ToV25()
        {
            return Pib27To25.Convert(this);
        }

        /*
        public Pib29 ToV29()
        {
            return Pib27To29.Convert(this);
        }
        */
    }
}
