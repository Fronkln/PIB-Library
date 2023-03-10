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

            using (DataStream stream = DataStreamFactory.FromMemory())
            {
                DataWriter writer = new DataWriter(stream) { Endianness = EndiannessMode.BigEndian };
                writer.Write(emitter.UnknownMainData);
                writer.Stream.Position = 72;
                writer.Insert(28);
                writer.Write(-7372);
                writer.Write(-4975);
                writer.Write(-7372);
                writer.Write(-4915);


                /*
                writer.Write(0.9999999f);
                writer.WriteTimes(0, 24);
                */

                emitter.UnknownMainData = writer.Stream.ToArray();
            }

            return emitter;
        }
    }
}
