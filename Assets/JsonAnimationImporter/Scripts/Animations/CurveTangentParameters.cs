// using JsonAnimationImporter.LottieAnimationSerializable;
// using UnityEngine;
//
// namespace Game.Scripts.JsonAnimationImporter {
//     public readonly struct CurveTangentParameters {
//         public readonly Vector2 InTangent;
//         public readonly Vector2 OutTangent;
//         public readonly Vector2 InWeight;
//         public readonly Vector2 OutWeight;
//
//         public CurveTangentParameters(LottieKeyframe lottieKeyframe) {
//             if (lottieKeyframe.Ti != null) {
//                 InTangent = new Vector2((float)lottieKeyframe.Ti[0], (float)lottieKeyframe.Ti[1]);
//             }
//             else {
//                 InTangent = Vector2.zero;
//             }
//
//             if (lottieKeyframe.To != null) {
//                 OutTangent = new Vector2((float)lottieKeyframe.To[0], (float)lottieKeyframe.To[1]);
//             }
//             else {
//                 OutTangent = Vector2.zero;
//             }
//
//             if (lottieKeyframe.InTangent != null) {
//                 InWeight = new Vector2(lottieKeyframe.InTangent.ValueX, lottieKeyframe.InTangent.ValueY);
//             }
//             else {
//                 InWeight = Vector2.zero;
//             }
//
//             if (lottieKeyframe.OutTangent != null) {
//                 OutWeight = new Vector2(lottieKeyframe.OutTangent.ValueX, lottieKeyframe.OutTangent.ValueY);
//             }
//             else {
//                 OutWeight = Vector2.zero;
//             }
//         }
//     }
// }