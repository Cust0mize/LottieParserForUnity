using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class Maker {
        [JsonProperty("tm")] public double TimeMarker { get; private set; }
        [JsonProperty("cm")] public string Comment { get; private set; }
        [JsonProperty("dr")] public double Duration { get; private set; }
    }
}