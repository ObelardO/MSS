using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorBasePosition : EditorGenericTween<TweenBase, int>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "f";
        public override bool Multiple => true;
        public override float Height => 28;

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorBasePosition(), EditorGUI.IntField);

        #region Inspector
        
        public override void Draw(Rect rect, TweenBase tween)
        {
            //tween.Value = DrawValueFunc(rect, DisplayName, tween.Value);
            //DrawValue(tween, rect, EditorGUI.IntField);
        }

        #endregion
    }
}
