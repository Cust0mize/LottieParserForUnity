using JsonAnimationImporter.LottieAnimationSerializable;
using JsonAnimationImporter.Scripts.Animations;
using Game.Scripts.JsonAnimationImporter;
using Game.Scripts.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

namespace JsonAnimationImporter.Scripts {
    public class TestImporter : MonoBehaviour {
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private Vector2 _anchoredPosition;
        [SerializeField] private Vector2 _pivot;
        [SerializeField] private string _savePath;

        private readonly AnimationPlayer _animationPlayer = new AnimationPlayer(new BaseAnimation[] { new PositionAnimation(), new ScaleAnimation() });
        private readonly Dictionary<Image, Layer> _animations = new Dictionary<Image, Layer>();


        private AnimationAssetCreator _animationAssetCreator;
        private LottieAnimationParser _lottieAnimationParser;
        private LayerHandler _layerHandler;

        [Button]
        public void StartImport() {
            _animations.Clear();
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

                if (_animations.ContainsKey(imageComponent)) {
                    continue;
                }

                imageComponent.transform.SetAsFirstSibling();
                _animations.Add(imageComponent, currentLayer);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [Button]
        public void PlayAnimation() {
            foreach ((Image image, Layer layer) in _animations) {
                _animationPlayer.PlayAnimation(layer.Transform, image);
            }
        }
    }

    public class AnimationPlayer {
        private readonly BaseAnimation[] _animations;

        public AnimationPlayer(BaseAnimation[] animations) {
            _animations = animations;
        }

        public void PlayAnimation(TransformProperties animationParameter, Image image) {
            for (int i = 0; i < _animations.Length; i++) {
                _animations[i].PlayAnimation(animationParameter, image).Forget();
            }
        }
    }
}