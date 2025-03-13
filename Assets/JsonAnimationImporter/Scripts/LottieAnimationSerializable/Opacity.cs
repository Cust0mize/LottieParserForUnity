using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class Opacity {
        [JsonProperty("a")] public int Animated { get; private set; }
        [JsonProperty("k")] public IAnimationParameter Value { get; private set; }
        [JsonProperty("ix")] public int Index { get; private set; }
    }
}