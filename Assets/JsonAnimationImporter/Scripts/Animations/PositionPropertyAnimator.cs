using JsonAnimationImporter.LottieAnimationSerializable;
using UnityEngine;

namespace Game.Scripts.JsonAnimationImporter {
    public class PositionPropertyAnimator : BasePropertyAnimator {
        protected override string AnimationParameter => "m_AnchoredPosition";

        protected override IAnimationParameter Animation(TransformProperties transformProperties) {
            Position positionProperty = transformProperties.Position;
            return positionProperty.Values;

            if (positionProperty.Values is AnimatedAnimationParameter animatedAnimationParameter) {
                for (int j = 0; j < animatedAnimationParameter.Keyframes.Count; j++) {
                    LottieKeyframe currentKey = animatedAnimationParameter.Keyframes[j];
                    CurveTangentParameters tangentParameters = new CurveTangentParameters(currentKey);
                    float time = (float)currentKey.Time / 60;
                    float xValue = (float)currentKey.Value[0];
                    float yValue = (float)currentKey.Value[1];
                    Vector2 inTangent = new Vector2(tangentParameters.InTangent.x, tangentParameters.InTangent.y * -1);
                    Vector2 outTangent = new Vector2(tangentParameters.OutTangent.x, tangentParameters.OutTangent.y * -1);
                    Vector2 inWeight = new Vector2(tangentParameters.InWeight.x, tangentParameters.InWeight.y);
                    Vector2 outWeight = new Vector2(tangentParameters.OutWeight.x, tangentParameters.OutWeight.y);
                    float multiplier = 2.2f;
                    // SetAnimationCurveValues(xTime, xValue, -yValue, inTangent, outTangent, inWeight * multiplier, outWeight * multiplier);
                    SetAnimationCurveValues(time, xValue, -yValue);
                }
            }
            else if (positionProperty.Values is StaticAnimationParameter staticAnimationParameter) {
                Vector3 position = GetVectorFromStaticAnimationParameters(staticAnimationParameter);
                SetAnimationCurveValues(0, position.x, -position.y);
            }
            else if (positionProperty.Values is StaticSingleValue staticSingleValue) {
                SetAnimationCurveValues(0, (float)staticSingleValue.Values, -(float)staticSingleValue.Values);
            }
        }
    }
}