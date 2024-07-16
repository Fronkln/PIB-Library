using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib21To19
    {
        public static Pib19 Convert(Pib21 pib21)
        {
            Pib19 pib = new Pib19();
            pib.Version = PibVersion.Y3;

            pib21.CopyFields(pib);

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv21 emitter in pib21.Emitters)
                pib.Emitters.Add(Emitter21To19.Convert(emitter));

            return pib;
        }
    }
}
