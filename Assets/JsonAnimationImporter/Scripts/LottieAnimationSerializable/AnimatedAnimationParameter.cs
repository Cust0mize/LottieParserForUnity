using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class AnimatedAnimationParameter : IAnimationParameter {
        [JsonProperty("k")] public List<LottieKeyframe> Keyframes { get; set; }
    }
}