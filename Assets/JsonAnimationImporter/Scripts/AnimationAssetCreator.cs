using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace Game.Scripts.JsonAnimationImporter {
    public class AnimationAssetCreator {
        private readonly Dictionary<string, Image> _imageAssets;
        private readonly LottieAnimation _lottieAnimation;
        private readonly Transform _creatorRoot;
        private readonly string _savePath;

        private AnimationClip _animationClip;
        private Transform _animationRoot;

        public AnimationClip AnimationClip => _animationClip;

        private const string ANIMATION_SAVE_PATH = "Assets/LottieAnimations";

        public AnimationAssetCreator(LottieAnimation animation, Transform creatorRoot, string savePath) {
            _lottieAnimation = animation;
            _savePath = savePath;
            _creatorRoot = creatorRoot;
            _imageAssets = new Dictionary<string, Image>();
        }

        public void Install(Vector2 anchoredPosition, Vector2 pivot) {
            CreateRootAndSetup();
            CreateImages(anchoredPosition, pivot);
        }

        public Image GetImageComponentByID(string assetId) {
            if (assetId != null && _imageAssets.ContainsKey(assetId)) {
                return _imageAssets[assetId];
            }

            Debug.LogWarning($"None of the assets with id: {assetId}");
            return null;
        }

        private void CreateRootAndSetup() {
            GameObject instanceRoot = new GameObject(_lottieAnimation.Name, typeof(RectTransform));
            // instanceRoot.AddComponent<Animator>();
            // Animation animationComponent = instanceRoot.AddComponent<Animation>();
            // _animationClip = CreateAndGetAnimationClip(instanceRoot, _lottieAnimation);
            // animationComponent.clip = _animationClip;
            instanceRoot.transform.SetParent(_creatorRoot, false);
            RectTransform instanceRect = instanceRoot.transform as RectTransform;
            instanceRect.sizeDelta = new Vector2((float)_lottieAnimation.Width, (float)_lottieAnimation.Height);
            _animationRoot = instanceRoot.transform;
        }

        private void CreateImages(Vector2 anchoredPosition, Vector2 pivot) {
            for (int i = 0; i < _lottieAnimation.Assets.Count; i++) {
                IAsset currentImageAsset = _lottieAnimation.Assets[i];

                if (currentImageAsset is ImageAsset imageAsset) {
                    GameObject assetInstance = new GameObject(imageAsset.AssetID);
                    assetInstance.transform.SetParent(_animationRoot, false);
                    Image imageComponent = assetInstance.AddComponent<Image>();
                    byte[] bytes = CreateImage(imageAsset.Path, _savePath + $@"\{imageAsset.AssetID}.png");
                    Texture2D texture = new Texture2D((int)imageAsset.Width, (int)imageAsset.Height);
                    texture.LoadImage(bytes);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    imageComponent.sprite = sprite;
                    imageComponent.preserveAspect = true;
                    imageComponent.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
                    imageComponent.rectTransform.anchorMin = anchoredPosition;
                    imageComponent.rectTransform.anchorMax = anchoredPosition;
                    imageComponent.rectTransform.pivot = pivot;
                    _imageAssets.Add(imageAsset.AssetID, imageComponent);
                }
            }
        }

        private AnimationClip CreateAndGetAnimationClip(GameObject targetObject, LottieAnimation animation) {
            if (!AssetDatabase.IsValidFolder(ANIMATION_SAVE_PATH)) {
                AssetDatabase.CreateFolder("Assets", "LottieAnimations");
            }

            string animationClipPath = $"{ANIMATION_SAVE_PATH}/{animation.Name}.anim";
            string controllerPath = $"{ANIMATION_SAVE_PATH}/{animation.Name}_Controller.controller";
            AnimationClip animationClip = new AnimationClip { legacy = false };
            AssetDatabase.CreateAsset(animationClip, animationClipPath);
            AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
            AnimatorState state = animatorController.layers[0].stateMachine.AddState(animation.Name);
            state.motion = animationClip;
            Animator animator = targetObject.GetComponent<Animator>();

            if (!animator) {
                animator = targetObject.AddComponent<Animator>();
            }

            animator.runtimeAnimatorController = animatorController;
            animationClip.frameRate = (float)animation.FrameRate;
            return animationClip;
        }

        private byte[] CreateImage(string base64Image, string savePath) {
            if (base64Image == null) {
                return null;
            }

            if (base64Image.StartsWith("data:image/png;base64,")) {
                base64Image = base64Image.Replace("data:image/png;base64,", "");
            }
            else if (base64Image.StartsWith("data:image/jpeg;base64,")) {
                base64Image = base64Image.Replace("data:image/jpeg;base64,", "");
            }

            byte[] imageBytes = Convert.FromBase64String(base64Image);
            File.WriteAllBytes(savePath, imageBytes);
            return imageBytes;
        }
    }
}