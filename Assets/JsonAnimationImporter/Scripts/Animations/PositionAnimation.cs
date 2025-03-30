using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;
using Game.Scripts.Extensions;
using Cysharp.Threading.Tasks;
using Bezier.Scripts.Models;
using Bezier.Scripts.Mono;
using JsonAnimationImporter.Scripts.LottieAnimationSerializable;
using UnityEngine.UI;
using UnityEngine;

namespace JsonAnimationImporter.Scripts.Animations {
    public class PositionAnimation : BaseAnimation {
        public override async UniTaskVoid PlayAnimation(TransformProperties transformProperties, Image image) {
            if (transformProperties.Position.Values is AnimatedAnimationParameter animatedAnimationParameter) {
                List<LottieKeyframe> keyFrames = animatedAnimationParameter.Keyframes;

                for (int i = 0; i < keyFrames.Count - 1; i++) {
                    LottieKeyframe currentKey = keyFrames[i];

                    if (currentKey.Time < 0) {
                        continue;
                    }

                    LottieKeyframe nextKey = keyFrames[i + 1];
                    await UniTask.DelayFrame((int)currentKey.Time);
                    float startTime = (float)currentKey.Time / 60f;
                    float endTime = (float)nextKey.Time / 60f;
                    float currentTime = startTime;
                    BezierLineModel bezierLineModel = GetBezierLineModel(currentKey, nextKey);
                    image.rectTransform.anchoredPosition = bezierLineModel.Get2DPoint(0).VerticesPoint;

                    while (currentTime < endTime) {
                        float currentLerpValue = currentTime.Map(startTime, endTime, 0, 1);
                        image.rectTransform.anchoredPosition = bezierLineModel.Get2DPoint(currentLerpValue).VerticesPoint;
                        currentTime += Time.deltaTime;
                        await UniTask.Yield();
                    }
                }
            }
            else if (transformProperties.Position.Values is StaticAnimationParameter staticAnimationParameter) {
                Vector2 currentPosition = staticAnimationParameter.Values.ConvertToVector2();
                Vector2 currentPositionInverseY = new Vector2(currentPosition.x, currentPosition.y * -1);
                image.rectTransform.anchoredPosition = currentPositionInverseY;
            }
            else if (transformProperties.Position.Values is StaticSingleValue staticSingleValue) {
                Vector2 currentPosition = new Vector2((float)staticSingleValue.Values, (float)staticSingleValue.Values);
                Vector2 currentPositionInverseY = new Vector2(currentPosition.x, currentPosition.y * -1);
                image.rectTransform.anchoredPosition = currentPositionInverseY;
            }
        }

        private BezierLineModel GetBezierLineModel(LottieKeyframe currentKeyPosition, LottieKeyframe nextKeyPosition) {
            GetBezierPositions(currentKeyPosition, nextKeyPosition, out Vector2 mainStartPointPosition, out Vector2 mainEndPointPosition, out Vector2 helpStartPointPosition, out Vector2 helpEndPointPosition);
            BezierPointModel bezierStartPoint = new BezierPointModel(mainStartPointPosition, helpStartPointPosition);
            BezierPointModel bezierEndPoint = new BezierPointModel(mainEndPointPosition, helpEndPointPosition);
            BezierElementModel startElement = new BezierElementModel(bezierStartPoint, bezierEndPoint);
            BezierResolutionSettings bezierResolutionSettings = new BezierResolutionSettings(60, new int[1] { 60 });
            OffsetSettings offsetSettings = new OffsetSettings(0.5f, 0.5f);
            BezierLineModel bezierLineModel = new BezierLineModel(new BezierElementModel[] { startElement }, bezierResolutionSettings, offsetSettings);
            return bezierLineModel;
        }

        private void GetBezierPositions(
        LottieKeyframe currentKeyPosition,
        LottieKeyframe nextKeyPosition,
        out Vector2 mainStartPointPosition,
        out Vector2 mainEndPointPosition,
        out Vector2 helpStartPointPosition,
        out Vector2 helpEndPointPosition
        ) {
            mainStartPointPosition = GetInverseYPosition(currentKeyPosition.Value.ConvertToVector2());
            mainEndPointPosition = GetInverseYPosition(nextKeyPosition.Value.ConvertToVector2());
            helpStartPointPosition = mainStartPointPosition + GetInverseYPosition(currentKeyPosition.TangentOutput.ConvertToVector2());
            helpEndPointPosition = mainEndPointPosition + GetInverseYPosition(currentKeyPosition.TangentInput.ConvertToVector2());
        }

        private Vector2 GetInverseYPosition(Vector2 position) {
            return new Vector2(position.x, -position.y);
        }
    }
}