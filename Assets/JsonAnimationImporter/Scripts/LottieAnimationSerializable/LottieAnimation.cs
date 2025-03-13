using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public class LottieAnimation {
        [JsonProperty("v")] public string Version { get; private set; }

        [JsonProperty("fr")] public double FrameRate { get; private set; }

        [JsonProperty("ip")] public double InPoint { get; private set; }

        [JsonProperty("op")] public double OutPoint { get; private set; }

        [JsonProperty("w")] public double Width { get; private set; }

        [JsonProperty("h")] public double Height { get; private set; }

        [JsonProperty("nm")] public string Name { get; private set; }

        [JsonProperty("ddd")] public int ThreeDimensional { get; private set; }

        [JsonProperty("assets")] public List<IAsset> Assets { get; private set; }

        [JsonProperty("layers")] public List<Layer> Layers { get; private set; }

        [JsonProperty("markers")] public List<Maker> Markers { get; private set; }

        [JsonProperty("props")] public Dictionary<string, object> Properties { get; private set; }
    }
}