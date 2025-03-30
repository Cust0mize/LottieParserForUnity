using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Game.Scripts.JsonAnimationImporter {
    public class PropertyAnimationController {
        private Dictionary<Type, BasePropertyAnimator> _animators;

        public PropertyAnimationController() {
            _animators = new Dictionary<Type, BasePropertyAnimator>()
            {
                { typeof(PositionPropertyAnimator), new PositionPropertyAnimator() },
                { typeof(ScalePropertyAnimator), new ScalePropertyAnimator() }
            };
        }

        public IEnumerable<ImageAnimationElement> StartAnimation(TransformProperties transformProperties, AnimationClip animationClip, Image imageComponent) {
            foreach (KeyValuePair<Type, BasePropertyAnimator> animator in _animators) {
                // IAnimationParameter animationParameter = _animators[animator.Key].StartAnimation(transformProperties, animationClip, imageComponent);
                ImageAnimationElement imageAnimationElement = new ImageAnimationElement(animator.Value, transformProperties);
                yield return imageAnimationElement;
            }
        }
    }

    public record ImageAnimationElement {
        public BasePropertyAnimator BasePropertyAnimator { get; }
        public TransformProperties TransformProperties { get; }

        public ImageAnimationElement(BasePropertyAnimator basePropertyAnimator, TransformProperties animationParameter) {
            BasePropertyAnimator = basePropertyAnimator;
            TransformProperties = animationParameter;
        }
    }
}