using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace PIBLib.Conversions
{
    internal static class Emitter25To21
    {
        public static PibEmitterv21 Convert(PibEmitterv25 emitter25)
        {
            PibEmitterv21 emitter = new PibEmitterv21();
            emitter25.CopyFields(emitter);

            emitter.Metaball = new PibBaseMetaball();
            emitter25.Metaball.CopyFields(emitter.Metaball);

            EmitterFlag1v27 flags = (EmitterFlag1v27)emitter25.Flags;
            EmitterFlag3v27 flags3 = (EmitterFlag3v27)emitter25.Flags3;
            EmitterFlag4v27 flags4 = (EmitterFlag4v27)emitter25.OOEUnkStructure6.Flag4;

            int v21Flags2 = 0;
            int v21Flags3 = 0;
            int v21Flags4 = 0;

            //Metaball only blend correction, else makes spit partices look really odd
            if (flags.HasFlag(EmitterFlag1v27.eFLG_METABALL))
            {
                if (emitter25.Blend == 1)
                    emitter.Blend = 0;
            }

            if (flags.HasFlag(EmitterFlag1v27.eFLG_METABALL))
                if (flags.HasFlag(EmitterFlag1v27.Flag6))
                    emitter.Blend = 1;

            //Convert flags3 to v21 flags2
            foreach (Enum flag in flags3.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int v21Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag2v21), flagStr));

                    v21Flags2 |= v21Value;
                }
                catch
                {
                    Console.WriteLine("Flag3 not found v25-v21 " + flag.ToString());
                }
            }


            if (v21Flags2.HasFlag((int)EmitterFlag2v21.UNK_V21_FLAG8))
            {
                v21Flags3 |= (int)(1 << 0);
                v21Flags3 |= (int)(1 << 1);
                v21Flags2 |= (int)EmitterFlag2v21.UNK_V21_FLAG6;
            }

            if (flags4.HasFlag(EmitterFlag4v27.Flag9))
            {
                flags4 &= ~EmitterFlag4v27.Flag9;
            }

            //Convert flags4 to v21 flags2
            foreach (Enum flag in flags4.GetFlags())
            {
                try
                {
                    string flagStr = flag.ToString();
                    int v21Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag4v21), flagStr));

                    v21Flags4 |= v21Value;
                }
                catch
                {
                    Console.WriteLine($"Flag 4 not found v25-v21 " + flag.ToString());
                }
            }

            /*
            if(emitter.Flags2.HasFlag(8))
            {
                emitter.Flags2 = emitter.Flags2.RemoveFlag(8);

                if (emitter.Type == 5)
                {
                    if (emitter.Blend == 0)
                        emitter.Flags2 |= 32;
                    else
                    {
                        emitter.Flags2 |= 16;
                        emitter.Flags3 = 1;
                    }
                }
            }
            */

            if (emitter.Flags3.HasFlag(1 << 3))
            {
               // emitter.OOEUnkStructure1.Unknown1 = emitter25.UnkReg1[0];
                //emitter.OOEUnkStructure1.Unknown2 = emitter25.UnkReg1[1];
                //emitter.OOEUnkStructure1.Unknown3 = emitter25.UnkReg1[2];
            }

            //GOOFY ASS CHECKS AHEAD!!!
            //https://twitter.com/Nghiemduy84/status/1704901164788232349


            /*
            if (emitter.Blend == 0 && emitter.Flags3 == 0)
                emitter.Flags3 = 3;

            if (emitter.Flags.HasFlag(1 << 23))
            {
                if (emitter.OEUnknownFlag4 > 97)
                    emitter.OEUnknownFlag4 = 97;
                
                if(emitter.Flags2 == 1)
                    emitter.Blend = 0;

                if(emitter.Blend == 1)
                {
                    //Is HasFlag(1) needed? Not sure. But i included it to prevent ruining other pibs.
                    if (emitter.Flags2.HasFlag(16) && emitter.Flags3.HasFlag(1))
                    {
                        //YYi0031 converted to Y5 had this, caused crashes. Emitter 4
                        emitter.Flags2 = emitter.Flags2.RemoveFlag(16);
                    }
                }
            }

            
            if(emitter.Flags2.HasFlag(128))
            {
                //YYi0022 converted to Y5 had this, caused crashes. Emitter 3
                emitter.Flags2 = emitter.Flags2.RemoveFlag(128);
            }

            if (emitter.Flags2.HasFlag(512))
            {
                //YYi0022 converted to Y5 had this, caused crashes. Emitter 4
                emitter.Flags2 = emitter.Flags2.RemoveFlag(512);
            }

            if(emitter.GetEmitterType() == EmitterType.Model)
            {
                //Yyj0014 converted to Y5 had this, caused crashes. Emitter 1
                if (emitter.Flags2.HasFlag(1024))
                    if(!emitter.Flags2.HasFlag(256))
                        emitter.Flags2 |= 256;
            }
            */

            emitter.Flags2 = v21Flags2;
            emitter.Flags3 = v21Flags3;
            emitter.OOEUnkStructure6.Flag4 = v21Flags4;

            return emitter;
        }
    }
}
