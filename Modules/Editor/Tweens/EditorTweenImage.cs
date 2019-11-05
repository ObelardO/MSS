using UnityEditor;
using UnityEngine;
using Obel.MSS.Editor;
using UnityEngine.UI;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenImage : EditorGenericTween<TweenImage, Graphic, Color>
    {
        #region Properties

        public override string Name => "UI/Image";
        public override bool IsMultiple => true;    
        public override bool ShowValueFuncContent => true;
        public override float Height => EditorConfig.Sizes.SingleLine;

        private static readonly GUIContent contentRaycastTarget = new GUIContent("Raycast Target");

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenImage(), EditorGUI.ColorField, new GUIContent("Color"));

        #endregion

        #region Inspector
        
        
        public override void Draw(Rect rect, TweenImage tween)
        {
            EditorLayout.PropertyField(rect, ref tween.IsRaycastTarget, EditorGUI.Toggle, InspectorStates.Record, contentRaycastTarget);
        }
        

        #endregion
    }
}
