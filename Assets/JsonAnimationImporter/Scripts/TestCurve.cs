using UnityEngine;
using UnityEditor; // Для отрисовки тангенсов через Handles

namespace Game.Scripts.JsonAnimationImporter {
    [ExecuteAlways]
    public class TestCurve : MonoBehaviour {
        [SerializeField] private AnimationCurve _animationCurve;

        [SerializeField] private float _time;
        [SerializeField] private float _firstValue;
        [SerializeField] private Vector2 _inTangent;
        [SerializeField] private Vector2 _outTangent;
        [SerializeField] private Vector2 _inWeight;
        [SerializeField] private Vector2 _outWeight;

        [SerializeField] private float _time2;
        [SerializeField] private float _firstValue2;
        [SerializeField] private Vector2 _inTangent2;
        [SerializeField] private Vector2 _outTangent2;
        [SerializeField] private Vector2 _inWeight2;
        [SerializeField] private Vector2 _outWeight2;

        private void Update() {
            _animationCurve = new AnimationCurve();
            Keyframe keyframe = new Keyframe(_time, _firstValue, _inTangent.x, _inTangent.y, _inWeight.x, _inWeight.y);
            Keyframe keyframe2 = new Keyframe(_time2, _firstValue2, _inTangent2.x, _inTangent2.y, _inWeight2.x, _inWeight2.y);
            _animationCurve.AddKey(keyframe);
            _animationCurve.AddKey(keyframe2);
        }

        private void OnDrawGizmos() {
            if (_animationCurve == null || _animationCurve.length < 2) return;

            Gizmos.color = Color.green;
            Vector3 previousPoint = transform.position;

            const int steps = 100;
            float stepSize = 1f / steps;

            for (int i = 1; i <= steps; i++) {
                float time = i * stepSize;
                float value = _animationCurve.Evaluate(time);

                Vector3 nextPoint = transform.position + new Vector3(time * 10f, value * 2f, 0); // Масштабирование для наглядности
                Gizmos.DrawLine(previousPoint, nextPoint);

                previousPoint = nextPoint;
            }

            DrawTangents();
        }

        private void DrawTangents() {
            if (_animationCurve == null || _animationCurve.length < 2) return;

            Gizmos.color = Color.red;

            // Первая точка
            Vector3 point1 = transform.position + new Vector3(_time * 10f, _firstValue * 2f, 0);
            Vector3 inTangent1 = point1 - new Vector3(_inTangent.x, _inTangent.y, 0).normalized * 2f;
            Vector3 outTangent1 = point1 + new Vector3(_outTangent.x, _outTangent.y, 0).normalized * 2f;

            Gizmos.DrawLine(point1, inTangent1);   // InTangent
            Gizmos.DrawLine(point1, outTangent1);  // OutTangent

            // Вторая точка
            Vector3 point2 = transform.position + new Vector3(_time2 * 10f, _firstValue2 * 2f, 0);
            Vector3 inTangent2 = point2 - new Vector3(_inTangent2.x, _inTangent2.y, 0).normalized * 2f;
            Vector3 outTangent2 = point2 + new Vector3(_outTangent2.x, _outTangent2.y, 0).normalized * 2f;

            Gizmos.DrawLine(point2, inTangent2);   // InTangent
            Gizmos.DrawLine(point2, outTangent2);  // OutTangent
        }
    }
}
