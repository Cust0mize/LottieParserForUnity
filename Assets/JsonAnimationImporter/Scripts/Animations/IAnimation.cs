using JsonAnimationImporter.LottieAnimationSerializable;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace JsonAnimationImporter.Scripts.Animations {
    public abstract class BaseAnimation {
        public abstract UniTaskVoid PlayAnimation(TransformProperties transformProperties, Image image);
    }
}