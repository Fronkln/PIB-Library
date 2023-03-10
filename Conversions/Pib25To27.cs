using System;
using System.Reflection;
using System.Collections.Generic;


namespace PIBLib.Conversions
{
    internal static class Pib25To27
    {
        public static Pib27 Convert(Pib25 pib25)
        {
            Pib27 pib = new Pib27();
            pib25.CopyFields(pib);
            pib.Version = PibVersion.Y0;

            pib.Emitters = new List<BasePibEmitter>();

            foreach(PibEmitterv25 emitter in pib25.Emitters)
                pib.Emitters.Add(Emitter25To27.Convert(emitter));

            return pib;
        }
    }
}
