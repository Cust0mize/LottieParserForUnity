using JsonAnimationImporter.LottieAnimationSerializable;
using Newtonsoft.Json;

namespace JsonAnimationImporter.Scripts.LottieAnimationSerializable {
    public class StaticSingleValue : IAnimationParameter {
        [JsonProperty("k")] public double Values { get; set; }
    }
}