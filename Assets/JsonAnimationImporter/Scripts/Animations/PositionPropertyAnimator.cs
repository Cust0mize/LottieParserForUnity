using JsonAnimationImporter.LottieAnimationSerializable;
using UnityEngine;

namespace Game.Scripts.JsonAnimationImporter {
    public class PositionPropertyAnimator : BasePropertyAnimator {
        protected override string AnimationParameter => "m_AnchoredPosition";

        protected override void Animation(TransformProperties transformProperties) {
            Position positionProperty = transformProperties.Position;

            if (positionProperty.Values is AnimatedAnimationParameter animatedAnimationParameter) {
                for (int j = 0; j < animatedAnimationParameter.Keyframes.Count; j++) {
                    LottieKeyframe currentKey = animatedAnimationParameter.Keyframes[j];
                    CurveTangentParameters tangentParameters = new CurveTangentParameters(currentKey);
                    float xTime = (float)currentKey.Time / 60;
                    float xValue = (float)currentKey.Value[0];
                    float yValue = (float)currentKey.Value[1];
                    Vector2 inTangent = tangentParameters.InTangent.normalized;
                    Vector2 outTangent = tangentParameters.OutTangent.normalized;

                    SetAnimationCurveValues(xTime, xValue, -yValue, inTangent, outTangent);
                }
            }
            else if (positionProperty.Values is StaticAnimationParameter staticAnimationParameter) {
                Vector3 position = GetVectorFromStaticAnimationParameters(staticAnimationParameter);
                SetAnimationCurveValues(0, position.x, -position.y, Vector2.zero, Vector2.zero, 0);
            }
            else if (positionProperty.Values is StaticSingleValue staticSingleValue) {
                SetAnimationCurveValues(0, (float)staticSingleValue.Values, -(float)staticSingleValue.Values, Vector2.zero, Vector2.zero, 0, 0);
            }
        }
    }
}