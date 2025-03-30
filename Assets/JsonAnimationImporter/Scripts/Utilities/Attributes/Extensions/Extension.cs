using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;
using System;
using System.Globalization;


#if UNITY_EDITOR
using UnityEditor.UIElements;
using UnityEditor;
#endif

namespace Game.Scripts.Extensions {
    public static class UniTaskProgressExtensions {
        public static async UniTask<T> AsProgressed<T>(
        this UniTask<T> task,
        IProgress<float> progress,
        float start,
        float end
        ) {
            float range = end - start;

            while (!task.Status.IsCompleted()) {
                await UniTask.Yield();
                progress.Report(start + (task.Status.IsCompleted() ? range : 0f));
            }

            T result = await task;
            progress.Report(end);
            return result;
        }
    }

    public static class DictionaryExtensions {
        public static void TryAddOrPlus<T1, T2>(this Dictionary<T1, List<T2>> keyValuePairs, T1 key, T2 value) {
            if (keyValuePairs.TryAdd(key, new List<T2> { value }) == false) {
                keyValuePairs[key].Add(value);
            }
        }

        public static void TryAddOrPlus<T1, T2>(this Dictionary<T1, Queue<T2>> keyValuePairs, T1 key, T2 value) {
            if (keyValuePairs.ContainsKey(key)) {
                keyValuePairs[key].Enqueue(value);
            }
            else {
                Queue<T2> queue = new Queue<T2>();
                queue.Enqueue(value);
                keyValuePairs.Add(key, queue);
            }
        }
    }

    public static class VisualElementExtension {
        public static void SetBorderColor(this VisualElement visualElement, Color targetColor) {
            visualElement.style.borderRightColor = targetColor;
            visualElement.style.borderLeftColor = targetColor;
            visualElement.style.borderTopColor = targetColor;
            visualElement.style.borderBottomColor = targetColor;
        }

        public static void SetBorderSize(this VisualElement visualElement, int newSize) {
            visualElement.style.borderRightWidth = newSize;
            visualElement.style.borderLeftWidth = newSize;
            visualElement.style.borderTopWidth = newSize;
            visualElement.style.borderBottomWidth = newSize;
        }

        public static void ChangeDisplayFlex(this VisualElement visualElement) {
            visualElement.style.display = visualElement.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    public static class ImageExtension {
        public static void SetAlpha(this UnityEngine.UI.Image image, float alpha) {
            Color currentColor = image.color;
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        }
    }

    public static class FloatExtension {
        public static float Map(
        this float value,
        float fromMin,
        float fromMax,
        float toMin,
        float toMax
        ) {
            return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
        }
    }

    public static class ListExtensions {
        public static T GetRandomItem<T>(this IList<T> list) {
            return list[Random.Range(0, list.Count)];
        }

        public static IEnumerable<EndT> TransformComponentsToType<StartT, EndT>(this IList<StartT> values) where StartT : Component where EndT : Component {
            for (int i = 0; i < values.Count; i++) {
                yield return values[i].GetComponent<EndT>();
            }
        }

        public static bool ContainsAnyValue<T>(this IList<T> list) {
            return list.Count > 0;
        }

        public static bool IsEmpty<T>(this IList<T> list) {
            return list.Count == 0;
        }

        public static void Shuffle<T>(this IList<T> list) {
            System.Random rng = new System.Random();

            for (int i = list.Count - 1; i > 0; i--) {
                int j = rng.Next(0, i + 1);

                T temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }

        public static bool HasDuplicate<T>(this List<T> list) {
            HashSet<T> elementsSet = new HashSet<T>(list);
            return elementsSet.Count != list.Count;
        }

        public static Vector3 ConvertToVector3<T>(this List<T> list) where T : struct, IConvertible {
            return new Vector3(list[0].ToSingle(CultureInfo.InvariantCulture), list[1].ToSingle(CultureInfo.InvariantCulture), list[2].ToSingle(CultureInfo.InvariantCulture));
        }

        public static Vector2 ConvertToVector2<T>(this List<T> list) where T : struct, IConvertible {
            return new Vector2(list[0].ToSingle(CultureInfo.InvariantCulture), list[1].ToSingle(CultureInfo.InvariantCulture));
        }
    }

    public static class LineRendererExtension {
        public static IEnumerable<Material> GetMaterial(this IEnumerable<LineRenderer> lineRenderers) {
            for (int i = 0; i < lineRenderers.Count(); i++) {
                yield return lineRenderers.ElementAt(i).material;
            }
        }
    }

    public static class TransformExtensions {
        public static void DestroyChildren(this Transform transform) {
            for (int i = transform.childCount - 1; i >= 0; i--) {
                Transform child = transform.GetChild(i);
                Object.Destroy(child.gameObject);
            }
        }

        public static void ResetTransformation(this Transform transform) {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static float GetCurrentValueBuAxis(this Transform transform, RotateAxis rotateAxis) {
            float result = 0f;

            switch (rotateAxis) {
                case RotateAxis.X:
                    result = transform.rotation.eulerAngles.x;
                    break;
                case RotateAxis.Y:
                    result = transform.rotation.eulerAngles.y;
                    break;
                case RotateAxis.Z:
                    result = transform.rotation.eulerAngles.z;
                    break;
            }

            return result;
        }

        public static void SetSmallSize(this Transform transform) {
            transform.localScale = Vector3.one * 0.01f;
        }
    }

    public static class MaterialExtension {
        public static void InstanceMaterial(this Material targetMaterial, Material instanceMaterial) {
            targetMaterial = new Material(instanceMaterial);
        }
    }

    public static class Vector3Extensions {
        public static Vector3 WithX(this Vector3 value, float x) {
            value.x = x;
            return value;
        }

        public static Vector3 WithY(this Vector3 value, float y) {
            value.y = y;
            return value;
        }

        public static Vector3 WithZ(this Vector3 value, float z) {
            value.z = z;
            return value;
        }

        public static Vector3 AddX(this Vector3 value, float x) {
            value.x += x;
            return value;
        }

        public static Vector3 AddY(this Vector3 value, float y) {
            value.y += y;
            return value;
        }

        public static Vector3 AddZ(this Vector3 value, float z) {
            value.z += z;
            return value;
        }

        public static Vector2[] ToVector2(this Vector3[] vector3s) {
            Vector2[] vectors = new Vector2[vector3s.Length];

            for (int i = 0; i < vectors.Length; i++) {
                vectors[i] = vector3s[i];
            }

            return vectors;
        }

        public static Vector3[] ToVector3(this Vector2[] vector2s) {
            Vector3[] vectors = new Vector3[vector2s.Length];

            for (int i = 0; i < vectors.Length; i++) {
                vectors[i] = vector2s[i];
            }

            return vectors;
        }
    }

    public static class ActionExtension {
        public static int GetSubscriberCount<T>(this Action<T> action) {
            return action?.GetInvocationList().Length ?? 0;
        }
    }

    public static class IntExtension {
        public static bool IsEven(this int value) {
            return value % 2 == 0;
        }

        public static bool IsLastIn<T>(this int value, T[] array) {
            return array.Length - 1 == value;
        }

        public static bool IsLastIn<T>(this int value, List<T> list) {
            return list.Count - 1 == value;
        }

        public static bool IsFirst(this int value) {
            return value == 0;
        }

        public static int GetSum(this int first, int second) {
            return first + second;
        }

        public static int GetDivide(this int first, int second) {
            if (first == 0) {
                return int.MinValue;
            }

            if (first % second == 0) {
                return first / second;
            }
            else {
                return int.MinValue;
            }
        }

        public static int GetSubstract(this int first, int second) {
            return first - second;
        }

        public static int GetMultiply(this int first, int second) {
            return first * second;
        }

        public static float ConvertFrameToTime(this int frameCount, int framePerSecond) {
            return (float)frameCount / framePerSecond;
        }
    }

    public static class StringExtension {
        public static string Reverse(this string text) {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    public static class BoolExtension {
        public static bool IsClear(this bool[] bools) {
            bool result = true;

            for (int i = 0; i < bools.Length; i++) {
                if (bools[i]) {
                    return false;
                }
            }

            return result;
        }
    }

    public static class ButtonExtension {
        public static void RemoveAllAndSubscribeButton(this UnityEngine.UI.Button button, UnityAction unityAction) {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(unityAction);
        }

        public static void RemoveAllAndSubscribeButtons(this UnityEngine.UI.Button[] buttons, UnityAction unityAction) {
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].RemoveAllAndSubscribeButton(unityAction);
            }
        }
    }

    public static class ToggleExtension {
        public static void RemoveAllAndAddListener(this UnityEngine.UI.Toggle button, UnityAction<bool> unityAction) {
            button.onValueChanged.RemoveAllListeners();
            button.onValueChanged.AddListener(unityAction);
        }
    }

    public static class SerializObjectExtension {
#if UNITY_EDITOR
        public static void BindProperty(
        this SerializedObject serializedObject,
        VisualElement root,
        string propertyName,
        string uiFieldName
        ) {
            SerializedProperty property = serializedObject.FindProperty(propertyName);
            PropertyField simpleMinField = root.Q<PropertyField>(uiFieldName);
            simpleMinField.BindProperty(property);
        }
#endif
    }

    public readonly struct UnityObjectHandler {
        public static void DisableObjects(bool state, params GameObject[] targetObjects) {
            for (int i = 0; i < targetObjects.Length; i++) {
                targetObjects[i].SetActive(state);
            }
        }
    }
}

public enum RotateAxis {
    X,
    Y,
    Z,
}