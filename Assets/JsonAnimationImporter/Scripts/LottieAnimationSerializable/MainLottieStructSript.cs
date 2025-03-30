using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine;
using System;

namespace JsonAnimationImporter.LottieAnimationSerializable {
    public interface IAnimationParameter {
    }

    public interface IAsset {
    }

    public class PositionConverter : JsonConverter<IAnimationParameter> {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, IAnimationParameter value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override IAnimationParameter ReadJson(
        JsonReader reader,
        Type objectType,
        IAnimationParameter existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
        ) {
            JToken jsonObject = JToken.Load(reader);

            if (jsonObject.Type == JTokenType.Array) {
                if (jsonObject.First.Type == JTokenType.Object) {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        Converters = new List<JsonConverter> { new ControlPointConverter() },
                        Error = (sender, args) =>
                        {
                            Debug.LogError($"JSON Error: {args.ErrorContext.Error.Message} at Path: {args.ErrorContext.Path}");
                            args.ErrorContext.Handled = true;
                        }
                    };

                    List<LottieKeyframe> keyFrames = JsonConvert.DeserializeObject<List<LottieKeyframe>>(jsonObject.ToString(), settings);
                    AnimatedAnimationParameter result = new AnimatedAnimationParameter();
                    result.Keyframes = keyFrames;
                    return result;
                }
                else if (jsonObject.First.Type == JTokenType.Float || jsonObject.First.Type == JTokenType.Integer) {
                    return new StaticAnimationParameter { Values = jsonObject.ToObject<List<double>>() };
                }
            }
            else if (jsonObject.Type == JTokenType.Float || jsonObject.Type == JTokenType.Integer) {
                double value = jsonObject.ToObject<double>();
                return new StaticSingleValue { Values = value };
            }

            throw new JsonSerializationException("Unexpected token type for IPositionValue");
        }
    }

    public class ControlPointConverter : JsonConverter<IControlPoint> {
        public override void WriteJson(JsonWriter writer, IControlPoint value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override IControlPoint ReadJson(
        JsonReader reader,
        Type objectType,
        IControlPoint existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
        ) {
            JToken jsonObject = JToken.Load(reader);
            JTokenType targetEqualType = jsonObject.First.First.Type;

            if (targetEqualType == JTokenType.Array) {
                ListControlPoint listControlPoint = jsonObject.ToObject<ListControlPoint>();
                return listControlPoint;
            }
            else if (targetEqualType == JTokenType.Float || targetEqualType == JTokenType.Integer) {
                ValueControlPoint valueControlPoint = jsonObject.ToObject<ValueControlPoint>();
                return valueControlPoint;
            }

            throw new JsonSerializationException("Unexpected token type for IPositionValue");
        }
    }

    public class AssetConverter : JsonConverter<IAsset> {
        public override void WriteJson(JsonWriter writer, IAsset value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override IAsset ReadJson(
        JsonReader reader,
        Type objectType,
        IAsset existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
        ) {
            JToken jsonObject = JToken.Load(reader);
            string assetID = jsonObject["id"]?.ToString();

            if (assetID != null && assetID.Contains("comp")) {
                string name = jsonObject["nm"].ToString();
                double frameRate = jsonObject["fr"].ToObject<double>();
                string layerData = jsonObject["layers"].ToString();


                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Converters = new List<JsonConverter> { new PositionConverter() },
                    Error = (sender, args) =>
                    {
                        Debug.LogError($"JSON Error: {args.ErrorContext.Error.Message} at Path: {args.ErrorContext.Path}");
                        args.ErrorContext.Handled = true;
                    }
                };

                List<Layer> layers = JsonConvert.DeserializeObject<List<Layer>>(jsonObject["layers"].ToString(), settings);
                CompositionAsset resultObject = new CompositionAsset(assetID, name, frameRate, layers);
                return resultObject;
            }
            else {
                return jsonObject.ToObject<ImageAsset>();
            }

            throw new JsonSerializationException("Unexpected token type for IAsset");
        }
    }
}