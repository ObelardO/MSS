using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorBasePosition : EditorGenericTween<TweenBase>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "f";
        public override bool Multiple => true;
        public override float Height => 28;

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorBasePosition());

        #region Inspector
        
        public override void Draw(Rect rect, TweenBase tween)
        {
            DrawValue(tween, rect, EditorGUI.IntField);
        }

        #endregion
    }
}
