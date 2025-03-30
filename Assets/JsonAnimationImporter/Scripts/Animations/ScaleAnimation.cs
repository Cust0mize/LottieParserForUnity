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
                    Vector2 startSize = GetScaleValue(transformProperties.AnchorPoint, configStartSize);
                    Vector2 endSize = GetScaleValue(transformProperties.AnchorPoint, configEndSize);
                    image.rectTransform.sizeDelta = startSize;

                    while (currentTime < endTime) {
                        float currentLerpValue = currentTime.Map(startTime, endTime, 0, 1);
                        image.rectTransform.sizeDelta = Vector2.Lerp(startSize, endSize, currentLerpValue);
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
    }
}