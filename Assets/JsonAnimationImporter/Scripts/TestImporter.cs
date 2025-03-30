using JsonAnimationImporter.LottieAnimationSerializable;
using Game.Scripts.Utilities.Attributes;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
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

        private Dictionary<Image, IEnumerable<ImageAnimationElement>> _animations = new Dictionary<Image, IEnumerable<ImageAnimationElement>>();
        private AnimationPlayer _animationPlayer = new AnimationPlayer(new IAnimation[] { new PositionAnimation(), new ScaleAnimation() });


        private PropertyAnimationController _propertyAnimationController;
        private AnimationAssetCreator _animationAssetCreator;
        private LottieAnimationParser _lottieAnimationParser;
        private LayerHandler _layerHandler;

        [Button]
        public void StartImport() {
            _animations.Clear();
            _propertyAnimationController = new PropertyAnimationController();
            _lottieAnimationParser = new LottieAnimationParser(_textAsset.text);
            _lottieAnimationParser.ParseLottinAnimation();
            _animationAssetCreator = new AnimationAssetCreator(_lottieAnimationParser.LottieAnimation, transform, _savePath);
            _animationAssetCreator.Install(_anchoredPosition, _pivot);
            _layerHandler = new LayerHandler(_lottieAnimationParser.LottieAnimation);
            _layerHandler.FindAllLayers();

            for (int i = 0; i < _layerHandler.AnimationLayersCount; i++) {
                Layer currentLayer = _layerHandler.GetLayerByIndex(i);
                Image imageComponent = _animationAssetCreator.GetImageComponentByID(currentLayer.ReferenceId);

                if (imageComponent == null) {
                    continue;
                } 

                IEnumerable<ImageAnimationElement> animationParameters = _propertyAnimationController.StartAnimation(currentLayer.Transform, _animationAssetCreator.AnimationClip, imageComponent);

                if (_animations.ContainsKey(imageComponent)) {
                    continue;
                }

                _animations.Add(imageComponent, animationParameters);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [Button]
        public void PlayAnimation() {
            foreach (KeyValuePair<Image, IEnumerable<ImageAnimationElement>> element in _animations) {
                Image image = element.Key;
                IEnumerable<ImageAnimationElement> animationParameters = element.Value;

                foreach (ImageAnimationElement animationParameter in animationParameters) {
                    _animationPlayer.PlayAnimation(animationParameter, image);
                }
            }
        }
    }

    public class AnimationPlayer {
        private readonly IAnimation[] _animations;

        public AnimationPlayer(IAnimation[] animations) {
            _animations = animations;
        }

        public void PlayAnimation(ImageAnimationElement animationParameter, Image image) {
            for (int i = 0; i < _animations.Length; i++) {
                _animations[i].PlayAnimation(animationParameter, image).Forget();
            }
        }
    }

    public interface IAnimation {
        public UniTaskVoid PlayAnimation(ImageAnimationElement animationParameter, Image image);
    }

    public class ScaleAnimation : IAnimation {
        public async UniTaskVoid PlayAnimation(ImageAnimationElement animationParameter, Image image) {
            if (animationParameter.BasePropertyAnimator is ScalePropertyAnimator scalePropertyAnimator) {
                if (animationParameter.TransformProperties.Scale.Values is StaticAnimationParameter staticAnimationParameter) {
                    Vector2 values = GetScaleValue(animationParameter.TransformProperties.AnchorPoint, (float)staticAnimationParameter.Values[0], (float)staticAnimationParameter.Values[1]);
                    image.rectTransform.sizeDelta = values;
                }
                else if (animationParameter.TransformProperties.Scale.Values is StaticSingleValue staticSingleValue) {
                    image.rectTransform.sizeDelta = new Vector2((float)staticSingleValue.Values, (float)staticSingleValue.Values);
                }
            }
        }

        private Vector2 GetScaleValue(AnchorPoint anchorPoint, float xPrecentScale, float yPrecentScale) {
            StaticAnimationParameter staticAnchornValues = anchorPoint.Values as StaticAnimationParameter;
            float xValue = (float)staticAnchornValues.Values[0] * 2 * (xPrecentScale / 100);
            float yValue = (float)staticAnchornValues.Values[1] * 2 * (yPrecentScale / 100);
            return new Vector2(xValue, yValue);
        }
    }
}