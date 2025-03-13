using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class TimeMapping {
        [JsonProperty("a")] public int Animated { get; private set; }
        [JsonProperty("k")] public IAnimationParameter Keyframes { get; private set; }
        [JsonProperty("ix")] public int Index { get; private set; }
    }
}