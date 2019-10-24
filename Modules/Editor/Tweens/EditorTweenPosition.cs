using UnityEditor;
using UnityEngine;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenPosition : EditorGenericTween<TweenPosition, Vector3>
    {
        #region Properties

        public override string Name => "T/Position";
        public override float Height => 32;

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenPosition(), EditorGUI.Vector3Field);

        #endregion

        #region Inspector

        public override void Draw(Rect rect, TweenPosition tween)
        {
            EditorGUI.DrawRect(rect, Color.red * 0.5f);
        }
        
        #endregion
    }
}
