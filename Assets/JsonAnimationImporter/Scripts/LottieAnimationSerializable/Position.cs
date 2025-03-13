using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class Position : IAnimatoinPropery {
        [JsonProperty("a")] public int Animated { get; private set; }
        [JsonProperty("k")] public IAnimationParameter Values { get; private set; }
        [JsonProperty("ix")] public int Index { get; private set; }
        [JsonProperty("l")] public int Linked { get; private set; }
    }
}