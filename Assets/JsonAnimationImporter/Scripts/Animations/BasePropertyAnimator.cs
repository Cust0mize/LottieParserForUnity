using JsonAnimationImporter.LottieAnimationSerializable;
using UnityEngine.UI;
using UnityEngine;

namespace Game.Scripts.JsonAnimationImporter {
    public abstract class BasePropertyAnimator {
        private AnimationCurve _animationCurveX;
        private AnimationCurve _animationCurveY;
        private AnimationClip _animationClip;
        protected abstract string AnimationParameter { get; }

        public IAnimationParameter StartAnimation(TransformProperties transformProperties, AnimationClip animationClip, Image imageComponent) {
            _animationClip = animationClip;
            _animationCurveX = new AnimationCurve();
            _animationCurveY = new AnimationCurve();
            return Animation(transformProperties);
            // SetCurveValue(objectPath);
        }

        protected abstract IAnimationParameter Animation(TransformProperties transformProperties);

        protected void SetCurveValue(string objectPath) {
            if (objectPath == null) {
                return;
            }

            _animationClip.SetCurve(objectPath, typeof(RectTransform), $"{AnimationParameter}.x", _animationCurveX);
            _animationClip.SetCurve(objectPath, typeof(RectTransform), $"{AnimationParameter}.y", _animationCurveY);
        }

        protected void SetAnimationCurveValues(
        float time,
        float xValue,
        float yValue,
        Vector2 inTangent = default,
        Vector2 outTangent = default,
        Vector2 inWeight = default,
        Vector2 outWeight = default
        ) {
            Keyframe xKeyFrame = new Keyframe(time, xValue, inTangent.x, outTangent.x, inWeight.x, outWeight.x);
            Keyframe yKeyFrame = new Keyframe(time, yValue, inTangent.y, outTangent.y, inWeight.y, outWeight.y);
            _animationCurveX.AddKey(xKeyFrame);
            _animationCurveY.AddKey(yKeyFrame);
        }

        protected Vector3 GetVectorFromStaticAnimationParameters(StaticAnimationParameter staticAnimationParameter) {
            float xValue = (float)staticAnimationParameter.Values[0];
            float yValue = (float)staticAnimationParameter.Values[1];
            float zValue = (float)staticAnimationParameter.Values[2];
            return new Vector3(xValue, yValue, zValue);
        }
    }
}