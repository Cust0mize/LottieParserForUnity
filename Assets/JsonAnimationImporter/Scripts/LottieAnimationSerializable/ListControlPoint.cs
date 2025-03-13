using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class ListControlPoint : IControlPoint {
        [JsonProperty("x")] public List<double> XPosition { get; private set; }
        [JsonProperty("y")] public List<double> YPosition { get; private set; }

        public float ValueX => (float)XPosition[0];
        public float ValueY => (float)YPosition[0];
    }
}