using Yarhl.IO.Serialization.Attributes;

namespace PIBLib
{
    [Serializable]
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2()
        {

        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 zero { get { return new Vector2(0, 0); } }
    }
}
