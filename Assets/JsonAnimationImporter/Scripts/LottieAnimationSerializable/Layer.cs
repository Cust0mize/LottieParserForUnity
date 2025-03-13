using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class Layer {
        [JsonProperty("ddd")] public int ThreeDimensional { get; set; }
        [JsonProperty("ind")] public int Index { get; set; }
        [JsonProperty("ty")] public int Type { get; set; }
        [JsonProperty("nm")] public string Name { get; set; }
        [JsonProperty("refId")] public string ReferenceId { get; set; }
        [JsonProperty("sr")] public double TimeStretch { get; set; }
        [JsonProperty("ks")] public TransformProperties Transform { get; set; }
        [JsonProperty("ao")] public int AutoOrient { get; set; }
        [JsonProperty("shapes")] public List<Shape> Shapes { get; set; }
        [JsonProperty("ip")] public double InPoint { get; set; }
        [JsonProperty("op")] public double OutPoint { get; set; }
        [JsonProperty("st")] public double StartTime { get; set; }
        [JsonProperty("bm")] public int BlendMode { get; set; }
        [JsonProperty("tm")] public TimeMapping TimeMapping { get; set; }
        [JsonProperty("cl")] public string Class { get; set; }
        [JsonProperty("parent")] public int? Parent { get; set; }
        [JsonProperty("w")] public double Width { get; set; }
        [JsonProperty("h")] public double Height { get; set; }
        [JsonProperty("ct")] public int ContentType { get; set; }
    }
}