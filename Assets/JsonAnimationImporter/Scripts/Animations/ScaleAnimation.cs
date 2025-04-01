using Bezier.Scripts.Models;
using Bezier.Scripts.Mono;
using JsonAnimationImporter.Scripts.LottieAnimationSerializable;
using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;
using Game.Scripts.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace JsonAnimationImporter.Scripts.Animations {
    public class ScaleAnimation : BaseAnimation {
        public override async UniTaskVoid PlayAnimation(TransformProperties transformProperties, Image image) {
            if (transformProperties.Scale.Values is AnimatedAnimationParameter animatedAnimationParameter) {
                List<LottieKeyframe> keyFrames = animatedAnimationParameter.Keyframes;

                for (int i = 0; i < keyFrames.Count - 1; i++) {
                    if (image.name == "image_0") {
                        Debug.Log("Brain");
                    }

                    LottieKeyframe currentKey = keyFrames[i];

                    if (currentKey.Time < 0) {
                        continue;
                    }

                    LottieKeyframe nextKey = keyFrames[i + 1];

                    if (i == 0) {
                        await UniTask.DelayFrame((int)currentKey.Time);
                    }

                    float startTime = (float)currentKey.Time / 60f;
                    float endTime = (float)nextKey.Time / 60f;
                    float currentTime = startTime;
                    Vector2 configStartSize = currentKey.Value.ConvertToVector2();
                    Vector2 configEndSize = nextKey.Value.ConvertToVector2();
                    Vector2 configStartHelpPointPosition = new Vector2(currentKey.OutputWeight.ValueX, currentKey.OutputWeight.ValueY);
                    Vector2 configEndHelpPointPosition = new Vector2(currentKey.InputWeight.ValueX, currentKey.InputWeight.ValueY);
                    Vector2 startSize = GetScaleValue(transformProperties.AnchorPoint, configStartSize);
                    Vector2 endSize = GetScaleValue(transformProperties.AnchorPoint, configEndSize);
                    Vector2 startHelp = GetScaleValue(transformProperties.AnchorPoint, configStartHelpPointPosition);
                    Vector2 endHelp = GetScaleValue(transformProperties.AnchorPoint, configEndHelpPointPosition);
                    image.rectTransform.sizeDelta = startSize;

                    BezierPointModel startTimePoint = new BezierPointModel(startSize, startSize + startHelp);
                    BezierPointModel endTimePoint = new BezierPointModel(endSize, endSize + endHelp);
                    BezierElementModel bezierElementModel = new BezierElementModel(startTimePoint, endTimePoint);
                    BezierResolutionSettings bezierResolutionSettings = new BezierResolutionSettings(60, new[] { 60 });
                    OffsetSettings offsetSettings = new OffsetSettings(1, 1);
                    BezierLineModel bezierLineModel = new BezierLineModel(new BezierElementModel[] { bezierElementModel }, bezierResolutionSettings, offsetSettings);

                    while (currentTime < endTime) {
                        if (image.name == "image_0") {
                            Debug.Log("Brain");
                        }

                        float mapTime = currentTime.Map(startTime, endTime, 0, 1);
                        Vector3 currentScale = bezierLineModel.Get2DPoint(mapTime).VerticesPoint;
                        image.rectTransform.sizeDelta = currentScale;
                        currentTime += Time.deltaTime;
                        await UniTask.Yield();
                    }
                }
            }
            else if (transformProperties.Scale.Values is StaticAnimationParameter staticAnimationParameter) {
                Vector2 configScale = staticAnimationParameter.Values.ConvertToVector2();
                Vector2 scale = GetScaleValue(transformProperties.AnchorPoint, configScale);
                image.rectTransform.sizeDelta = scale;
            }
            else if (transformProperties.Scale.Values is StaticSingleValue staticSingleValue) {
                Vector2 configSize = new Vector2((float)staticSingleValue.Values, (float)staticSingleValue.Values);
                Vector2 scale = GetScaleValue(transformProperties.AnchorPoint, configSize);
                image.rectTransform.sizeDelta = scale;
            }
        }

        private Vector2 GetScaleValue(AnchorPoint anchorPoint, Vector2 configSize) {
            StaticAnimationParameter anchorPointValue = anchorPoint.Values as StaticAnimationParameter;
            float xValue = (float)anchorPointValue.Values[0] * 2 * (configSize.x / 100);
            float yValue = (float)anchorPointValue.Values[1] * 2 * (configSize.y / 100);
            return new Vector2(xValue, yValue);
        }

        private float CubicBezierEasing(float t, Vector2 outTangent, Vector2 inTangent) {
            // Упрощённая версия кубической Безье-кривой
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector2 p0 = Vector2.zero;
            Vector2 p1 = outTangent; // исходящая касательная текущего ключа
            Vector2 p2 = inTangent; // входящая касательная следующего ключа
            Vector2 p3 = Vector2.one;

            // Формула кубической Безье-кривой
            float bezierT = uuu * p0.y + 3f * uu * t * p1.y + 3f * u * tt * p2.y + ttt * p3.y;
            return bezierT;
        }
    }
}