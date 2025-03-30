using UnityEngine;

namespace Game.Scripts.JsonAnimationImporter {
    public class TestCurve : MonoBehaviour {
        [SerializeField] private AnimationCurve _animationCurve;

        [SerializeField] private float _time;
        [SerializeField] private float _firstValue;
        [SerializeField] private float _secondValue;
        [SerializeField] private Vector2 _inTangent;
        [SerializeField] private Vector2 _outTangent;
        [SerializeField] private Vector2 _inWeight;
        [SerializeField] private Vector2 _outWeight;

        private void OnValidate() {
            Keyframe keyframe = new Keyframe(_time, _firstValue, _inTangent.x, _inTangent.y, _inWeight.x, _inWeight.y);
            _animationCurve.AddKey(keyframe);
        }
    }
}