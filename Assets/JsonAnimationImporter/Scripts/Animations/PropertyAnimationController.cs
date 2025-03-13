using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;
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

        public void StartAnimation(TransformProperties transformProperties, AnimationClip animationClip, string objectPath) {
            foreach (KeyValuePair<Type, BasePropertyAnimator> animator in _animators) {
                _animators[animator.Key].StartAnimation(transformProperties, animationClip, objectPath);
            }
        }
    }
}