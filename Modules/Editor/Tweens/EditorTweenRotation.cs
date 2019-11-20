using UnityEngine;
using UnityEditor;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenRotation : EditorGenericTween<TweenRotation, Transform, Quaternion>
    {
        #region Properties

        public override string Name => "T/Rotation";
        public override float Height => EditorConfig.Sizes.SingleLine;

        private static readonly GUIContent contentLocal = new GUIContent("Is local");

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenRotation(), QuaternionField, GUIContent.none);

        #endregion

        #region Inspector

        public override void Draw(Rect rect, TweenRotation tween)
        {
            EditorLayout.PropertyField(rect, ref tween.IsLocal, EditorGUI.Toggle, InspectorStates.Record, contentLocal);
        }

        private static Quaternion QuaternionField(Rect rect, GUIContent content, Quaternion value)
        {
            Vector3 eulerRotation = value.eulerAngles;

            EditorLayout.PropertyField(rect, ref eulerRotation, EditorGUI.Vector3Field, InspectorStates.Record, content);

            return Quaternion.Euler(eulerRotation);
        }

        #endregion
    }
}
