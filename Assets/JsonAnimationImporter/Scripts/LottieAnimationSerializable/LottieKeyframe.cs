using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class LottieKeyframe {
        [JsonProperty("i")] public IControlPoint InputWeight { get; private set; }
        [JsonProperty("o")] public IControlPoint OutputWeight { get; private set; }
        [JsonProperty("t")] public double Time { get; private set; }
        [JsonProperty("s")] public List<double> Value { get; private set; }
        [JsonProperty("to")] public List<double> TangentOutput { get; private set; }
        [JsonProperty("ti")] public List<double> TangentInput { get; private set; }
    }
}