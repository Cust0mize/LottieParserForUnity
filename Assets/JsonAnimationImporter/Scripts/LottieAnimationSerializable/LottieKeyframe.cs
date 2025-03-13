using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class LottieKeyframe {
        [JsonProperty("i")] public IControlPoint InTangent { get; private set; }
        [JsonProperty("o")] public IControlPoint OutTangent { get; private set; }
        [JsonProperty("t")] public double Time { get; private set; }
        [JsonProperty("s")] public List<double> Value { get; private set; }
        [JsonProperty("to")] public List<double> To { get; private set; }
        [JsonProperty("ti")] public List<double> Ti { get; private set; }
    }
}