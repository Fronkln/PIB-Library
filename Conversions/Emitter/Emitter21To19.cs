using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter21To19
    {
        public static PibEmitterv19 Convert(PibEmitterv21 emitter21)
        {
            PibEmitterv19 pibEmitter = new PibEmitterv19();
            emitter21.CopyFields(pibEmitter);

            pibEmitter.Metaball = new OOEPibMetaballv19();
            emitter21.Metaball.CopyFields(pibEmitter.Metaball);

            if (pibEmitter.OOEUnkStructure6.Unknown1 == 5)
                pibEmitter.OOEUnkStructure6.Unknown1 = 6;

            var ooeMetaball = emitter21.Metaball as OOEPibMetaballv19;
            var newMetaball = pibEmitter.Metaball as OOEPibMetaballv19;

            EmitterMetaballFlagv21 v21MetaballFlags = (EmitterMetaballFlagv21)ooeMetaball.Flags;
            int metaballFlags = 0;

            foreach (Enum flag in v21MetaballFlags.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterMetaballFlagv19), flagStr));

                    metaballFlags |= value;
                }
                catch
                {
                }
            }

            newMetaball.Flags = metaballFlags;

            /*
            if (pibEmitter.OOEUnkStructure6.MetaballFlags.HasFlag(64))
            {
                pibEmitter.OOEUnkStructure6.MetaballFlags &= ~64;
                pibEmitter.OOEUnkStructure6.MetaballFlags |= 16;
            }
            */


            //These flags dont exist in Y3. Were added on Y5
            for (int i = 0x1A; i < 0x20; i++)
                pibEmitter.Flags = pibEmitter.Flags.RemoveFlag(1 << i);

            //maybe add metaball cond
            //pibEmitter.OOEUnkStructure6.Unknown3 = -0.01666667f;

            return pibEmitter;
        }
    }
}
