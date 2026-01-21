using System;
using System.Linq;
using System.Reflection;

namespace PIBLib.Conversions
{
    internal static class Emitter29To43
    {
        public static PibEmitterv43 Convert(PibEmitterv29 emitter29)
        {
            PibEmitterv43 emitter = new PibEmitterv43();
            emitter29.CopyFields(emitter);
            emitter.ParticleCount2 = emitter29.Source.Particles.Count;

            // CommonUnkStructure2 = new DEPibCommonUnkStructure2();
            // CommonUnkStructure2.Read(reader);

            emitter.UnkStructure5.Unk7 = (int)emitter.Source.Particles.OrderByDescending(x => x.Lifetime).ElementAt(0).Lifetime;

            var metaball = new DEPibMetaballv43();
            emitter.Metaball = metaball;
            emitter29.Metaball.CopyFields(emitter.Metaball);
            metaball.LtRatio = 1;
            metaball.HltPower = 2;
            metaball.HltIntensity = 1;

            var track = new DEPibTrackv43();
            emitter.Track = track;       
            emitter29.Track.CopyFields(emitter.Track);
            track.UpdateLife = (int)emitter29.Source.Particles.OrderByDescending(x => x.Lifetime).ElementAt(0).Lifetime;

            EmitterFlag1v29 flags = (EmitterFlag1v29)emitter29.Flags;
            EmitterFlag3v29 flags3 = (EmitterFlag3v29)emitter29.Flags3;
            int v43Flags = 0;
            int v43Flags3 = 0;

            flags &= ~(EmitterFlag1v29.eFLG_UNK_V21_FLAG);
            flags &= (EmitterFlag1v29)~(1 << 0xB);

            //Convert v29 flags to v43 flags
            foreach (Enum flag in flags.GetFlags())
            {

                System.Diagnostics.Debug.WriteLine(flag.ToString());
                string flagStr = flag.ToString();
                int v43Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag1v43), flagStr));

                v43Flags |= v43Value;
            }

            //Convert v29 flags3 to v43 flags3
            foreach (Enum flag in flags3.GetFlags())
            {
                string flagStr = flag.ToString();
                int v43Value = System.Convert.ToInt32(Enum.Parse(typeof(EmitterFlag3v43), flagStr));

                v43Flags3 |= v43Value;
            }

            emitter.Flags = v43Flags;
            emitter.Flags3 = v43Flags3;

            //We do not know how to determine when to fill out VF and null out UnkStructure5 consistently yet
            //So we're gonna fill the values no matter what as a default
            emitter.VF.VelocityScale = 1;
            emitter.VF.Life = 10;

            /*
            emitter.UnkStructure5.Unk8 = 0;
            emitter.UnkStructure5.Unk9 = 0;
            emitter.UnkStructure5.Unk10 = 0;
            emitter.UnkStructure5.Unk11 = 0;
            emitter.UnkStructure5.Unk12 = 0;
            emitter.UnkStructure5.Unk13 = 0;
            emitter.UnkStructure5.Unk14 = 0;
            emitter.UnkStructure5.Unk15 = 0;
            emitter.UnkStructure5.Unk16 = 0;
            emitter.UnkStructure5.Unk17 = 0;
            emitter.UnkStructure5.Unk18 = 0;
            emitter.UnkStructure5.Unk19 = 0;
            emitter.UnkStructure5.Unk20 = 0;
            emitter.UnkStructure5.Unk21 = 0;
            emitter.UnkStructure5.Unk22 = 0;
            emitter.UnkStructure5.Unk23 = 0;
            emitter.UnkStructure5.Unk24 = 0;
            */

            if (emitter.DDSHeader.TextureFormat == 6)
                emitter.DDSHeader.TextureFormat = 8;

           foreach(var ptc in emitter.Source.Particles)
            {
                float oldTimeScale = ptc.TimeScale;
                float oldUnkTimeScale = ptc.UnknownTimeScaleThing;

                ptc.TimeScale *= 3000f;
                ptc.UnknownTimeScaleThing = (float)Math.Round(oldUnkTimeScale / oldTimeScale, 5);
            }

            PibEmitterAnimationCurveGeneric newCurve1 = new PibEmitterAnimationCurveGeneric() { Values = new float[emitter.PropertyAnimationCurve[0].GetDataSize() / 4] };
            PibEmitterAnimationCurveRGBA32F newCurve2 = new PibEmitterAnimationCurveRGBA32F() { Values = new RGBA32F[emitter.PropertyAnimationCurve[0].GetDataSize() / 16] };
            PibEmitterAnimationCurveGeneric newCurve3 = new PibEmitterAnimationCurveGeneric() { Values = new float[emitter.PropertyAnimationCurve[0].GetDataSize() / 4] };

            for (int i = 0; i < newCurve1.Values.Length; i++)
                newCurve1.Values[i] = 1f;

            for(int i = 0; i < newCurve2.Values.Length; i++)
            {
                RGBA32F col = new RGBA32F();
                col.A = 1f;

                newCurve2.Values[i] = col;
            }

            emitter.PropertyAnimationCurve.Insert(2, newCurve1);
           // emitter.PropertyAnimationCurve.Add(newCurve2);
            emitter.PropertyAnimationCurve.Add(newCurve3);

            emitter.TextureShaderIndices = new int[emitter.Textures.Count];

            int start = 4;

            for(int i = 0; i < emitter.TextureShaderIndices.Length; i++ )
            {
                emitter.TextureShaderIndices[i] = start;
                start++;
            }

            return emitter;
        }
    }
}
