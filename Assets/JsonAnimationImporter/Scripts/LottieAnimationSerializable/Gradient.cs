using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class Gradient {
        [JsonProperty("p")] public int Points { get; private set; }
        [JsonProperty("k")] public GradientValues Values { get; private set; }
    }
}