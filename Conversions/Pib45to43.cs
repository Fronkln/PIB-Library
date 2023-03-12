using System;
using System.Collections.Generic;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Pib45to43
    {
        public static Pib43 Convert(Pib45 pib45)
        {
            Pib43 pib = new Pib43();
            pib45.CopyFields(pib);
            pib.Version = PibVersion.YK2;

            //Align header data
            List<byte> Unknown_0x29C_expanded = new List<byte>(pib.Unknown_0x29C);
            Unknown_0x29C_expanded.InsertRange(0, new byte[] { 0, 0, 0, 0 , 0, 0, 0, 0});
            pib.Unknown_0x29C = Unknown_0x29C_expanded.ToArray();

            pib.Emitters = new List<BasePibEmitter>();

            foreach (PibEmitterv45 emitter in pib45.Emitters)
                pib.Emitters.Add(Emitter45To43.Convert(emitter));

            return pib;
        }
    }
}
