using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIBLib
{
    [Yarhl.IO.Serialization.Attributes.Serializable]
    public class DEPibEmitterBlurModule
    {
        public float BlurVelocityScale { get; set; }
        public float BlurAlphaScale { get; set; }
        public uint BlurNum { get; set; }
        public float BlurAlphaS { get; set; }
        public float BlurAlphaE { get; set; }
        public float BlurSec { get; set; }
    }
}
