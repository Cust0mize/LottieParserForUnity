using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class StaticSingleValue : IAnimationParameter {
        [JsonProperty("k")] public double Values { get; set; }
    }
}