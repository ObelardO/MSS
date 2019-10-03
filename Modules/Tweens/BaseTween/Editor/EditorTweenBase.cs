using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorBasePosition : EditorGenericTween<TweenBase, int>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "Empty tween";
        public override bool Multiple => true;
        public override float Height => 32;

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorBasePosition());

        #region Inspector
        
        public override void Draw(Rect rect, TweenBase tween)
        {
            EditorGUI.DrawRect(rect, Color.green * 0.5f);
            EditorGUI.LabelField(rect, "testring string");
        }

        #endregion
    }
}
