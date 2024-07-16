using System;
using System.Reflection;
using System.Collections.Generic;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter43to29
    {
        public static PibEmitterv29 Convert(PibEmitterv43 emitter43)
        {
            PibEmitterv29 emitter = new PibEmitterv29();
            emitter43.CopyFields(emitter);

            emitter.Metaball = new DEPibBaseMetaball();
            emitter43.Metaball.CopyFields(emitter.Metaball);

            emitter.Track = new DEPibBaseTrack();
            emitter43.Track.CopyFields(emitter.Track);

            EmitterFlag1v43 flags = (EmitterFlag1v43)emitter43.Flags;
            EmitterFlag3v43 flags3 = (EmitterFlag3v43)emitter43.Flags3;
            uint v29Flags = 0;
            uint v29Flags3 = 0;

            flags &= ~EmitterFlag1v43.eFLG_UNK_V43_FLAG;

            //Convert v43 flags to v29 flags
            foreach (Enum flag in flags.GetFlags())
            {
                string flagStr = flag.ToString();
                uint v29Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag1v29), flagStr));

                v29Flags |= v29Value;
            }

            //Convert v43 flags3 to v29 flags3
            foreach (Enum flag in flags3.GetFlags())
            {
                string flagStr = flag.ToString();
                uint v29Value = System.Convert.ToUInt32(Enum.Parse(typeof(EmitterFlag3v29), flagStr));

                v29Flags3 |= v29Value;
            }

            emitter.Flags = (int)v29Flags;
            emitter.Flags3 = (int)v29Flags3;

            emitter.PropertyAnimationCurve.RemoveAt(7);
            emitter.PropertyAnimationCurve.RemoveAt(2);

            //???? yyj0004
            if (emitter.DDSHeader.dwHeight >= 8)
                emitter.DDSHeader.dwHeight = 6;

            if(emitter.Source is ParticleBillboardv29)
            {
                var billboardv29 = emitter.Source as ParticleBillboardv29;

                foreach (ParticleBillboardDatav29 billboardDat in billboardv29.Particles)
                {
                    billboardDat.TimeScale /= 3000f;
                    billboardDat.UnknownTimeScaleThing /= 1175f;
                }
            }

            return emitter;
        }
    }
}
