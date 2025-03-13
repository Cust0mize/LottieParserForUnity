using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.JsonAnimationImporter {
    public class LottieAnimationParser {
        public LottieAnimation LottieAnimation { get; private set; }
        private readonly string _jsonData;

        public LottieAnimationParser(string jsonData) {
            _jsonData = jsonData;
        }

        public void ParseLottinAnimation() {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Converters = new List<JsonConverter> { new PositionConverter(), new AssetConverter(), new ControlPointConverter() },
                Error = (sender, args) =>
                {
                    Debug.LogError($"JSON Error: {args.ErrorContext.Error.Message} at Path: {args.ErrorContext.Path}");
                    args.ErrorContext.Handled = true;
                }
            };

            LottieAnimation = JsonConvert.DeserializeObject<LottieAnimation>(_jsonData, settings);
        }
    }
}