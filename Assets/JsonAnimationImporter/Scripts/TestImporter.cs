using JsonAnimationImporter.LottieAnimationSerializable;
using Game.Scripts.Utilities.Attributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Scripts.JsonAnimationImporter {
    public class TestImporter : MonoBehaviour {
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private Vector2 _anchoredPosition;
        [SerializeField] private Vector2 _pivot;
        [SerializeField] private string _savePath;

        private PropertyAnimationController _propertyAnimationController;
        private AnimationAssetCreator _animationAssetCreator;
        private LottieAnimationParser _lottieAnimationParser;
        private LayerHandler _layerHandler;

        [Button]
        public void StartImport() {
            _propertyAnimationController = new PropertyAnimationController();
            _lottieAnimationParser = new LottieAnimationParser(_textAsset.text);
            _lottieAnimationParser.ParseLottinAnimation();
            _animationAssetCreator = new AnimationAssetCreator(_lottieAnimationParser.LottieAnimation, transform, _savePath);
            _animationAssetCreator.Install(_anchoredPosition, _pivot);
            _layerHandler = new LayerHandler(_lottieAnimationParser.LottieAnimation);
            _layerHandler.FindAllLayers();

            for (int i = 0; i < _layerHandler.AnimationLayersCount; i++) {
                Layer currentLayer = _layerHandler.GetLayerByIndex(i);

                if (currentLayer.ReferenceId == "image_6") {
                    Debug.Log("check");
                }

                _propertyAnimationController.StartAnimation(currentLayer.Transform, _animationAssetCreator.AnimationClip, currentLayer.ReferenceId);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    public abstract class BasePropertyAnimator {
        private AnimationCurve _animationCurveX;
        private AnimationCurve _animationCurveY;
        private AnimationClip _animationClip;
        protected abstract string AnimationParameter { get; }

        public void StartAnimation(TransformProperties transformProperties, AnimationClip animationClip, string objectPath) {
            _animationClip = animationClip;
            _animationCurveX = new AnimationCurve();
            _animationCurveY = new AnimationCurve();
            Animation(transformProperties);
            SetCurveValue(objectPath);
        }

        protected abstract void Animation(TransformProperties transformProperties);

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
        Vector2 inTangent,
        Vector2 outTangent,
        float inWeightX = 0,
        float inWeightY = 0,
        float outWeightX = 0,
        float outWeightY = 0
        ) {
            Keyframe xKeyFrame = new Keyframe(time, xValue, inTangent.x, outTangent.x, inWeightX, outWeightX);
            Keyframe yKeyFrame = new Keyframe(time, yValue, inTangent.y, outTangent.y, inWeightY, outWeightY);
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

    public readonly struct CurveTangentParameters {
        public readonly Vector2 InTangent;
        public readonly Vector2 OutTangent;

        public readonly float InWeightX;
        public readonly float OutWeightX;
        public readonly float InWeightY;
        public readonly float OutWeightY;

        public CurveTangentParameters(LottieKeyframe lottieKeyframe) {
            if (lottieKeyframe.Ti != null && lottieKeyframe.Ti.Count > 0) {
                InTangent = new Vector2((float)lottieKeyframe.Ti[0], (float)lottieKeyframe.Ti[1]);
            }
            else {
                InTangent = Vector2.zero;
            }

            if (lottieKeyframe.To != null && lottieKeyframe.To.Count > 0) {
                OutTangent = new Vector2((float)lottieKeyframe.To[0], (float)lottieKeyframe.To[1]);
            }
            else {
                OutTangent = Vector2.zero;
            }

            if (lottieKeyframe.InTangent != null) {
                InWeightX = lottieKeyframe.InTangent.ValueX;
                InWeightY = lottieKeyframe.InTangent.ValueY;
            }
            else {
                InWeightX = 0;
                InWeightY = 0;
            }

            if (lottieKeyframe.OutTangent != null) {
                OutWeightX = lottieKeyframe.OutTangent.ValueX;
                OutWeightY = lottieKeyframe.OutTangent.ValueY;
            }
            else {
                OutWeightX = 0;
                OutWeightY = 0;
            }
        }
    }
}