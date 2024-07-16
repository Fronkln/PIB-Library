using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal class Pib52To58
    {
        public static Pib58 Convert(Pib52 pib52)
        {
            Pib58 pib = new Pib58();
            pib52.CopyFields(pib);
            pib.Flags = 0;
            pib.SoundCuesheet = 0;
            pib.Version = PibVersion.LJ;

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv52 emitter in pib52.Emitters)
                pib.Emitters.Add(Emitter52To58.Convert(emitter));

            return pib;
        }
    }
}
