using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class ControlPoints {
        [JsonProperty("x")] public List<double> X { get; private set; }
        [JsonProperty("y")] public List<double> Y { get; private set; }
    }
}