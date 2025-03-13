using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class TransformProperties {
        [JsonProperty("o")] public Opacity Opacity { get; private set; }
        [JsonProperty("r")] public Rotation Rotation { get; private set; }
        [JsonProperty("p")] public Position Position { get; private set; }
        [JsonProperty("a")] public AnchorPoint AnchorPoint { get; private set; }
        [JsonProperty("s")] public Scale Scale { get; private set; }
    }
}