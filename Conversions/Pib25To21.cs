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


            pib.UnknownFlag_0x38 = 128;

            if (pib.UnknownFlags_0x3E.HasFlag(1024))
            {
                pib.UnknownFlags_0x3E = pib.UnknownFlags_0x3E.RemoveFlag(1024);
                pib.UnknownFlags_0x3E |= 512;
            }

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv25 emitter in pib25.Emitters)
                pib.Emitters.Add(Emitter25To21.Convert(emitter));

            return pib;
        }
    }
}
