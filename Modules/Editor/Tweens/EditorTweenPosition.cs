using UnityEditor;
using UnityEngine;
using Obel.MSS.Editor;
using Obel.MSS.Modules.Tweens;

namespace Obel.MSS.Modules.TweensEditor
{
    internal class EditorTweenPosition : EditorGenericTween<TweenPosition, Vector3>
    {
        #region Properties

        public override string Name => "T/Position";
        public override float Height => 32;

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenPosition(), EditorGUI.Vector3Field);

        #region Inspector
        
        public override void Draw(Rect rect, TweenPosition tween)
        {
            EditorGUI.DrawRect(rect, Color.red * 0.5f);
        }
        
        #endregion
    }
}
