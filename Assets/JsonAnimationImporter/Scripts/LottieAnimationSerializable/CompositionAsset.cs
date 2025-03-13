using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public record CompositionAsset : IAsset {
        public CompositionAsset(
        string assetID,
        string compositionName,
        double compositionFrameRate,
        List<Layer> layers
        ) {
            CompositionFrameRate = compositionFrameRate;
            CompositionName = compositionName;
            AssetID = assetID;
            Layers = layers;
        }

        [JsonProperty("id")] public string AssetID { get; set; }
        [JsonProperty("nm")] public string CompositionName { get; set; }
        [JsonProperty("fr")] public double CompositionFrameRate { get; set; }
        [JsonProperty("layers")] public List<Layer> Layers { get; set; }
    }
}