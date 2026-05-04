using System;
using System.Collections.Generic;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Pib25To21
    {
        public static Pib21 Convert(Pib25 pib25)
        {
            Pib21 pib = new Pib21();
            pib.Version = PibVersion.Y5;

            pib25.CopyFields(pib);


            pib.UnknownV19 = 128;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv25 emitter in pib25.Emitters)
                pib.Emitters.Add(Emitter25To21.Convert(emitter));

            return pib;
        }
    }
}
