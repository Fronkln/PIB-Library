namespace PIBLib
{
    [Yarhl.IO.Serialization.Attributes.Serializable]
    public class PibFadeModule
    {
        public float FarFadeDistance { get; set; }
        public float FarFadeOffset { get; set; }
        public float NearFadeDistance { get; set; }
        public float NearFadeOffset { get; set; }
        public float NearFadeDistanceAll { get; set; }
        public float NearFadeOffsetAll { get; set; }
    }
}
