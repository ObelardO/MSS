using UnityEditor;
using UnityEngine;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenColor : EditorGenericTween<TweenColor, Color>
    {
        #region Properties

        public override string Name => "Color";
        public override bool IsMultiple => true;
        public override bool ShowValueFuncContent => true;
        public override float Height => 32;

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenColor(), EditorGUI.ColorField);

        #endregion

        #region Inspector
        
        /*
        public override void Draw(Rect rect, TweenColor tween)
        {
            EditorGUI.DrawRect(rect, Color.blue);
        }
        */

        #endregion
    }
}
