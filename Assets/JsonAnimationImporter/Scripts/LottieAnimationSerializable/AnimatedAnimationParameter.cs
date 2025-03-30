using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class AnimatedAnimationParameter : IAnimationParameter {
        [JsonProperty("k")] public List<LottieKeyframe> Keyframes { get; set; }
    }
}