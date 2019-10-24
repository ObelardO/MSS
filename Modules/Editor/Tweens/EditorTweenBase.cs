using UnityEditor;
using UnityEngine;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorBasePosition : EditorGenericTween<TweenBase, int>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "Empty tween";
        public override bool IsMultiple => true;
        public override float Height => 32;

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorBasePosition());

        #endregion

        #region Inspector

        public override void Draw(Rect rect, TweenBase tween)
        {
            EditorGUI.DrawRect(rect, Color.green * 0.5f);
            EditorGUI.LabelField(rect, "testing string");
        }

        #endregion
    }
}