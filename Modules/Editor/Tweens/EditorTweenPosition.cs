using UnityEngine;
using UnityEditor;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenPosition : EditorGenericTween<TweenPosition, Transform, Vector3>
    {
        #region Properties

        public override string Name => "T/Position";
        public override float Height => EditorConfig.Sizes.SingleLine;

        private static readonly GUIContent contentLocal = new GUIContent("Is local");

        #endregion

        #region Init

        //[InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenPosition(), EditorGUI.Vector3Field, GUIContent.none);

        #endregion

        #region Inspector

        public override void Draw(Rect rect, TweenPosition tween)
        {
            EditorLayout.PropertyField(rect, ref tween.IsLocal, EditorGUI.Toggle, InspectorStates.Record, contentLocal);
        }

        #endregion
    }
}
