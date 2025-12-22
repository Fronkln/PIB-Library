using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    public class ParticleIns
    {
        public RGBA Color = new RGBA(255, 255, 255, 255);
        public float Lifetime;
        public byte[] Unknown2 = new byte[12];
        public float UnknownTimeScaleThing;
        public byte[] Unknown6 = new byte[12];
        public float TimeScale = 0.0003333333f;
    }
}
