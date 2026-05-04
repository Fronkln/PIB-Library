using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Emitter25To21
    {
        public static PibEmitterv21 Convert(PibEmitterv25 emitter25)
        {
            PibEmitterv21 emitter = new PibEmitterv21();
            emitter25.CopyFields(emitter);
                
            emitter.Metaball = new OOEPibMetaballv19();
            emitter25.Metaball.CopyFields(emitter.Metaball);

            int flag2 = emitter.Flags3;
            int flag3 = emitter.Flags2;

            EmitterFlag3v27 oldFlag2 = (EmitterFlag3v27)flag2;
            int newFlag2 = 0;

            foreach (Enum flag in oldFlag2.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag2v21), flagStr));

                    newFlag2 |= value;
                }
                catch
                {
                }
            }


            //hmm???
            if ((flag2 & (int)EmitterFlag3v27.Flag8) != 0)
                newFlag2 |= (int)
                    EmitterFlag2v21.UNK_V21_FLAG8;

            //flag8 depends on 6 and will crash if 6 is unticked
            if (newFlag2.HasFlag((int)EmitterFlag2v21.UNK_V21_FLAG8))
                newFlag2 = newFlag2.SetFlag((int)EmitterFlag2v21.UNK_V21_FLAG6);

            if (emitter.Flags.HasFlag((int)EmitterFlag1v27.eFLG_UNK_V21_FLAG))
            {
                //flag8 depends on 6 and will crash if 6 is unticked
                if (!newFlag2.HasFlag((int)EmitterFlag2v21.UNK_V21_FLAG8))
                    newFlag2 = newFlag2.RemoveFlag((int)EmitterFlag2v21.UNK_V21_FLAG6);
            }


            //   if (emitter.Blend == 1)
            //    newFlag2 = newFlag2.RemoveFlag(32);


            //This is really, really really bad: Heuteristically calculate blend mode

            //Every pib in Yakuza 5 that had blend on 3 had flag3 at 24
            if (emitter.Blend == 3)
                flag3 = 24;


            bool haveWeirdFlagComb = (emitter.Flags & 20) != 0;

            emitter.Flags2 = newFlag2;
            emitter.Flags3 = flag3;

            var ooeMetaball = emitter25.Metaball as OOEPibMetaballv19;
            var newMetaball = emitter.Metaball as OOEPibMetaballv19;

            EmitterMetaballFlagv25 v25MetaballFlags = (EmitterMetaballFlagv25)ooeMetaball.Flags;
            int metaballFlags = 0;

            foreach (Enum flag in v25MetaballFlags.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterMetaballFlagv21), flagStr));

                    metaballFlags |= value;
                }
                catch
                {
                }
            }

            newMetaball.Flags = metaballFlags;

            return emitter;
        }
    }
}
