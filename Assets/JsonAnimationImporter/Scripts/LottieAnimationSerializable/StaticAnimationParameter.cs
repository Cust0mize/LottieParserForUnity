using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class StaticAnimationParameter : IAnimationParameter {
        [JsonProperty("k")] public List<double> Values { get; set; }
    }
}