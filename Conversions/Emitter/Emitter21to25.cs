using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib.Conversions
{
    internal static class Emitter21to25
    {
        public static PibEmitterv25 Convert(PibEmitterv21 emitterv21)
        {
            PibEmitterv25 emitter = new PibEmitterv25();
            emitterv21.CopyFields(emitter);

            emitter.Metaball = new PibBaseMetaball();
            emitterv21.Metaball.CopyFields(emitter.Metaball);

            int flag2 = emitter.Flags3;
            int flag3 = emitter.Flags2;

            //Y5 and Y0 YAc0024
            if (flag3.HasFlag(1 << 5))
            {
                flag3 &= ~(1 << 5);
                flag3 |= 1 << 3;
            }

            //Y5 and Y0 YAc0024
            if (flag3.HasFlag(16))
                flag3 &= ~(16);

            emitter.Flags2 = flag2;
            emitter.Flags3 = flag3;

            //setting UnknownV27 improperly will cause pib to not be visible or broken

            if (emitter.Textures.FirstOrDefault(x => x.Contains("_nml")) != null)
                emitter.TextureFlags |= 96;

            //Y5 and Y0 YAc0024, just an assumption
            if (emitter.Flags2.HasFlag(16))
            {
                emitter.TextureFlags |= 2;
                emitter.Flags2 &= ~16;
            }

            //Y5 and Y0 YAb0038, Emitter 1
            if (emitter.Flags2.HasFlag(1))
            {
                emitter.TextureFlags |= 32;
                emitter.Flags2 &= ~(1);
                emitter.Flags3 |= 8; //unsure about this one, YAb0038 Emitter 1
            }


            return emitter;
        }
    }
}
