using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class ShapeItem {
        [JsonProperty("ind")] public int LocalIndex { get; private set; }
        [JsonProperty("ty")] public string Type { get; private set; }
        [JsonProperty("ix")] public int GlobalIndex { get; private set; }
        [JsonProperty("ks")] public TransformProperties Transform { get; private set; }
        [JsonProperty("nm")] public string Name { get; private set; }
        [JsonProperty("mn")] public string MatchName { get; private set; }
        [JsonProperty("hd")] public bool Hidden { get; private set; }
        [JsonProperty("o")] public Opacity Opacity { get; private set; }
        [JsonProperty("r")] public int Rotation { get; private set; }
        [JsonProperty("bm")] public int BlendMode { get; private set; }
        [JsonProperty("g")] public Gradient Gradient { get; private set; }
        [JsonProperty("s")] public Scale Scale { get; private set; }
        [JsonProperty("e")] public End End { get; private set; }
        [JsonProperty("t")] public int TypeId { get; private set; }
    }
}