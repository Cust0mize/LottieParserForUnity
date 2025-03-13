using Newtonsoft.Json;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public record ImageAsset : IAsset {
        public ImageAsset(
        string assetID,
        double width,
        double height,
        string uri,
        string path,
        int embedded
        ) {
            AssetID = assetID;
            Width = width;
            Height = height;
            Uri = uri;
            Path = path;
            Embedded = embedded;
        }

        [JsonProperty("id")] public string AssetID { get; private set; }
        [JsonProperty("w")] public double Width { get; private set; }
        [JsonProperty("h")] public double Height { get; private set; }
        [JsonProperty("u")] public string Uri { get; private set; }
        [JsonProperty("p")] public string Path { get; private set; }
        [JsonProperty("e")] public int Embedded { get; private set; }
    }
}