namespace PIBLib
{
    public struct Matrix4x4
    {
        public Vector4 VM0;
        public Vector4 VM1;
        public Vector4 VM2;
        public Vector4 VM3;

        public static Matrix4x4 Default
        {
            get
            {
                Matrix4x4 mtx = new Matrix4x4();
                mtx.VM0 = new Vector4(1, 0, 0, 0);
                mtx.VM1 = new Vector4(0, 1, 0, 0);
                mtx.VM2 = new Vector4(0, 0, 1, 0);
                mtx.VM3 = new Vector4(0, 0, 0, 1);

                return mtx;
            }
        }
    }
}
