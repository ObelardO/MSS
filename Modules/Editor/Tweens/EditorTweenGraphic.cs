using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenGraphic : EditorGenericTween<TweenGraphic, Graphic, Color>
    {
        #region Properties

        public override string Name => "UI/Graphic";
        public override float Height => EditorConfig.Sizes.SingleLine;

        private static readonly GUIContent contentRaycastTarget = new GUIContent("Raycast Target");

        #endregion

        #region Init

        //[InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenGraphic(), EditorGUI.ColorField, new GUIContent("Color"));

        #endregion

        #region Inspector

        public override void Draw(Rect rect, TweenGraphic tween)
        {
            EditorLayout.PropertyField(rect, ref tween.IsRaycastTarget, EditorGUI.Toggle, InspectorStates.Record, contentRaycastTarget);
        }
        
        #endregion
    }
}
