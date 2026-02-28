using PIBLib.Conversions;
using System;
using Yarhl.IO;

namespace PIBLib
{
    //Everything same as Pib52, emitter grew by 32 bytes, flags changed
    public class Pib58 : Pib52
    {
        protected override void WriteHeader(DataWriter writer)
        {
            base.WriteHeader(writer);

            writer.Stream.RunInPosition(delegate
            {
                if (Version == PibVersion.Gaiden)
                    writer.Write(58);
            }, 8);
        }

        protected override void ReadEmitters(DataReader reader, int count)
        {
            for(int i = 0; i < count; i++)
            {
                PibEmitterv58 emitter = new PibEmitterv58();
                emitter.Read(reader, Version);
                Emitters.Add(emitter);
            }

            if(Emitters.Count > 0)
            {
                var emitterV58 = Emitters[0] as PibEmitterv58;

                if (emitterV58.TextureShaderIndices.Length > 0)
                    if (emitterV58.TextureShaderIndices[0] == 6)
                        Version = PibVersion.Gaiden;
            }
        }
        
        public Pib52 ToV52()
        {
            return Pib58To52.Convert(this);
        }

        public Pib59 ToV59()
        {
            return Pib58to59.Convert(this);
        }
    }
}
