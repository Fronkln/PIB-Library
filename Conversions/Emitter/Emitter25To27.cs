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
