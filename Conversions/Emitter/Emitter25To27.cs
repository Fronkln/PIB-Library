using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter25To27
    {
        public static PibEmitterv27 Convert(PibEmitterv25 emitterv25)
        {
            PibEmitterv27 emitter = new PibEmitterv27();
            emitterv25.CopyFields(emitter);

            //setting UnknownV27 improperly will cause pib to not be visible or broken

            if (emitter.Textures.FirstOrDefault(x => x.Contains("_nml")) != null)
                emitter.UnknownV27 |= 96;

            //Y5 and Y0 YAc0024, just an assumption
            if(emitter.Flags2.HasFlag(16))
            {
                emitter.UnknownV27 |= 2;
                emitter.Flags2 &= ~16;
            }

            //Y5 and Y0 YAb0038, Emitter 1
            if (emitter.Flags2.HasFlag(1))
            {
                emitter.UnknownV27 |= 32;
                emitter.Flags2 &= ~(1);
                emitter.Flags3 |= 8; //unsure about this one, YAb0038 Emitter 1
            }

            //Dont want to risk forcing a blend conversion on all 0 pibs yet
            //Y5 and Y0 YAb0038, Emitter 2
            if (emitter.Flags3.HasFlag(1))
            {
                if(emitter.Blend == 0)
                {
                    emitter.Blend = 1;
                    emitter.Flags3 |= 8;
                }
            }

            emitter.OEUnkStructure1 = new OEPibUnkStructure1v27();

            return emitter;
        }
    }
}
