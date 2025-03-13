using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

namespace Game.Scripts.Utilities.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute {

        public ButtonAttribute() {
        }
    }
}