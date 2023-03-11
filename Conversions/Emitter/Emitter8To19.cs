using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace PIBLib.Conversions
{
    internal static class Emitter8To19
    {
        public static PibEmitterv19 Convert(PibEmitterv8 emitter8)
        {
            PibEmitterv19 emitter = new PibEmitterv19();
            emitter8.CopyFields(emitter);

            using (DataStream stream = DataStreamFactory.FromMemory())
            {
                DataWriter writer = new DataWriter(stream) { Endianness = EndiannessMode.BigEndian };
                writer.Write(emitter8.UnknownMainData);
                
                writer.Stream.Position = 295;
                writer.Insert(32);
                writer.Stream.Position = 296;

                writer.Write(0);
                writer.Write(0.9999999f);
                writer.WriteTimes(0, 24);

                emitter.UnknownMainData = writer.Stream.ToArray();
            }

            return emitter;
        }
    }
}
