using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Pib19To21
    {
        public static Pib21 Convert(Pib19 pib19)
        {
            Pib21 pib = new Pib21();
            pib.Version = PibVersion.Y5;

            pib.ParticleID = pib19.ParticleID;
            pib.Duration = pib19.Duration;
            pib.Unknown1 = pib19.Unknown1;

            pib.Speed = pib19.Speed;
            pib.Unknown2 = pib19.Unknown2;
            pib.Unknown3 = pib19.Unknown3;

            pib.Unknown4 = pib19.Unknown4;
            pib.Unknown5 = pib19.Unknown5;

            pib.Unknown_0x34 = pib19.Unknown_0x34;
            pib.UnknownFlag_0x38 = pib19.UnknownFlag_0x38;
            pib.UnknownFlags_0x3E = 512;
            pib.BaseMatrix = pib19.BaseMatrix;
            pib.Scale = pib19.Scale;
            pib.Color = pib19.Color;

            pib.UnknownVector_0xB4 = new Vector2(-1, -1);

            foreach (PibEmitterv19 emitter in pib19.Emitters)
                pib.Emitters.Add(Emitter19To21.Convert(emitter));

            return pib;
        }
    }
}
