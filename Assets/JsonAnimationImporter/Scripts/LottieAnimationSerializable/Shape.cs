using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class Shape {
        [JsonProperty("ty")] public string Type { get; private set; }
        [JsonProperty("it")] public List<ShapeItem> Items { get; private set; }
        [JsonProperty("nm")] public string Name { get; private set; }
        [JsonProperty("np")] public int NumberOfPoints { get; private set; }
        [JsonProperty("cix")] public int ShapeIndex { get; private set; }
        [JsonProperty("bm")] public int BlendMode { get; private set; }
        [JsonProperty("ix")] public int Index { get; private set; }
        [JsonProperty("mn")] public string MatchName { get; private set; }
        [JsonProperty("hd")] public bool Hidden { get; private set; }
    }
}