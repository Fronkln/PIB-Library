using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib8To19
    {
        public static Pib19 Convert(Pib8 pib8)
        {
            Pib19 pib = new Pib19();
            pib.Version = PibVersion.Y3;
            pib.Scale = new Vector3(1, 1, 1);
            pib.BaseMatrix = Matrix4x4.Default;
            pib.Color = new RGB32(255, 255, 255);
            pib.Duration = pib8.Duration;
            pib.Speed = pib8.Speed;
           
            pib.Unknown4 = 0;
            pib.Unknown5 = 0;

            pib.UnknownFlag_0x38 = 128;

            foreach (PibEmitterv8 emitter in pib8.Emitters)
                pib.Emitters.Add(Emitter8To19.Convert(emitter));

            return pib;
        }
    }
}
