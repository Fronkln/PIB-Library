using System;

namespace PIBLib
{
    [Yarhl.IO.Serialization.Attributes.Serializable]
    public class DEPibLightModule
    {
        public float Shine { get; set; } = 0.5f;
        public float Ioe { get; set; } = 0.04f;
        public float LambertOffset { get; set; } = 0;
        public float Emissive { get; set; } = 0;
        public float Reflection { get; set; } = 1;
        public float Refraction { get; set; } = 0.02f;
    }
}
