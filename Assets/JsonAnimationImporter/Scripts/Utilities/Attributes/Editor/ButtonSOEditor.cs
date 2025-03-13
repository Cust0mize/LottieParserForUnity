using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Game.Scripts.Utilities.Attributes.Editors {
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ButtonSOEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            ScriptableObject targetSO = (ScriptableObject)target;
            MethodInfo[] methods = targetSO.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods) {
                if (method.GetCustomAttribute(typeof(ButtonAttribute)) != null) {
                    if (GUILayout.Button(method.Name)) {
                        method.Invoke(targetSO, null);
                    }
                }
            }
        }
    }
}