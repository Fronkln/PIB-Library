using System;
using System.Collections.Generic;
using System.Reflection;

namespace PIBLib.Conversions
{
    public class Pib45To52
    {

        public static Pib52 Convert(Pib45 pib45)
        {
            Pib52 pib = new Pib52();
            pib45.CopyFields(pib);
            pib.Version = PibVersion.YLAD;

            //Broke JE pibs on YLAD 
            //pib.Unknown1 = 0;

            pib.Emitters = new List<BasePibEmitter>();

            foreach(PibEmitterv45 emitter in pib45.Emitters)
            {
                pib.Emitters.Add(Emitter45To52.Convert(emitter));
            }

            return pib;
        }
    }
}
