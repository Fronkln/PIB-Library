using System;

namespace PIBLib
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public static Vector3 zero { get { return new Vector3(0, 0, 0); } }

        public override string ToString()
        {
            return $"{MathF.Round(x, 2)}, {MathF.Round(y, 2)}, {MathF.Round(z, 2)}";
        }
    }
}
