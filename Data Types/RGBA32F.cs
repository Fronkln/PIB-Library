using Yarhl.IO.Serialization.Attributes;

namespace PIBLib
{
    [Serializable]
    public class RGBA32F
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        public RGBA32F()
        {

        }

        public RGBA32F(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static implicit operator System.Drawing.Color(RGBA32F rgb32f)
        {
            byte r = (byte)(rgb32f.R * 255);
            byte g = (byte)(rgb32f.G * 255);
            byte b = (byte)(rgb32f.B * 255);
            byte a = (byte)(rgb32f.A * 255);

            System.Drawing.Color col = System.Drawing.Color.FromArgb(a,r,g,b);

            return col;
        }

        public static implicit operator RGBA32F(System.Drawing.Color rgba)
        {
            float r = (rgba.R / 255f);
            float g = (rgba.G / 255f);
            float b = (rgba.B / 255f);
            float a = (rgba.A / 255f);

            return new RGBA32F(r, g, b, a);
        }
    }
}
