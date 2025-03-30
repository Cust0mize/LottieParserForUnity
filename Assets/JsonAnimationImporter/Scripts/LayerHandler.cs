using JsonAnimationImporter.LottieAnimationSerializable;
using System.Collections.Generic;

namespace JsonAnimationImporter.Scripts {
    public class LayerHandler {
        private readonly LottieAnimation _lottieAnimation;
        private readonly List<Layer> _allAnimationLayers;

        public int AnimationLayersCount => _allAnimationLayers.Count;

        public LayerHandler(LottieAnimation lottieAnimation) {
            _lottieAnimation = lottieAnimation;
            _allAnimationLayers = new List<Layer>();
        }

        public void FindAllLayers() {
            _allAnimationLayers.AddRange(_lottieAnimation.Layers);

            for (int i = 0; i < _lottieAnimation.Assets.Count; i++) {
                IAsset currentAsset = _lottieAnimation.Assets[i];

                if (currentAsset is CompositionAsset compositionAsset) {
                    _allAnimationLayers.AddRange(compositionAsset.Layers);
                }
            }
        }

        public Layer GetLayerByIndex(int index) {
            return _allAnimationLayers[index];
        }
    }
}