using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class ValueControlPoint : IControlPoint {
        [JsonProperty("x")] public double XPosition { get; private set; }
        [JsonProperty("y")] public double YPosition { get; private set; }

        public float ValueX => (float)XPosition;
        public float ValueY => (float)YPosition;
    }
}