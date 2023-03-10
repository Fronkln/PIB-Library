namespace PIBLib
{
    public struct Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static implicit operator Vector3(Vector4 vec)
        {
            return new Vector3(vec.x, vec.y, vec.z);
        }

        public static implicit operator Vector4(Vector3 vec)
        {
            return new Vector4(vec.x, vec.y, vec.z, 0);
        }
    }
}
