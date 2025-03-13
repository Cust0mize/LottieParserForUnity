using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class TimeKeyframe {
        [JsonProperty("i")] public ControlPoints IncomingControlPoints { get; private set; }
        [JsonProperty("o")] public ControlPoints OutgoingControlPoints { get; private set; }
        [JsonProperty("t")] public double Time { get; private set; }
        [JsonProperty("s")] public List<double> Values { get; private set; }
    }
}