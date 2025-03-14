using JsonAnimationImporter.LottieAnimationSerializable;
using UnityEngine;

namespace Game.Scripts.JsonAnimationImporter {
    public class ScalePropertyAnimator : BasePropertyAnimator {
        protected override string AnimationParameter => "m_SizeDelta";

        protected override void Animation(TransformProperties transformProperties) {
            Scale scaleProperty = transformProperties.Scale;
            AnchorPoint anchorPoint = transformProperties.AnchorPoint;

            if (scaleProperty.Values is AnimatedAnimationParameter animatedAnimationParameter) {
                for (int j = 0; j < animatedAnimationParameter.Keyframes.Count; j++) {
                    LottieKeyframe currentKey = animatedAnimationParameter.Keyframes[j];
                    CurveTangentParameters tangentParameters = new CurveTangentParameters(currentKey);
                    float xTime = (float)currentKey.Time / 60;
                    float xValue = (float)currentKey.Value[0];
                    float yValue = (float)currentKey.Value[1];
                    Vector2 resultScale = GetValues(anchorPoint, xValue, yValue);
                    SetAnimationCurveValues(xTime, resultScale.x, resultScale.y, tangentParameters.InTangent, tangentParameters.OutTangent, tangentParameters.InWeight, tangentParameters.OutWeight);
                }
            }
            else if (scaleProperty.Values is StaticAnimationParameter staticAnimationParameter) {
                Vector3 position = GetVectorFromStaticAnimationParameters(staticAnimationParameter);
                Vector2 resultScale = GetValues(anchorPoint, position.x, position.y);
                SetAnimationCurveValues(0, resultScale.x, resultScale.y, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
            }
            else if (scaleProperty.Values is StaticSingleValue staticSingleValue) {
                Vector2 resultScale = GetValues(anchorPoint, (float)staticSingleValue.Values, (float)staticSingleValue.Values);
                SetAnimationCurveValues(0, resultScale.x, resultScale.y, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
            }
        }

        private Vector2 GetValues(AnchorPoint anchorPoint, float xScaleValue, float yScaleValue) {
            StaticAnimationParameter staticAnchornValues = anchorPoint.Values as StaticAnimationParameter;
            float xValue = (float)staticAnchornValues.Values[0] * 2 * (xScaleValue / 100);
            float yValue = (float)staticAnchornValues.Values[1] * 2 * (yScaleValue / 100);
            return new Vector2(xValue, yValue);
        }
    }
}