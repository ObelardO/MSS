using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorBasePosition : EditorGenericTween<TweenBase>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "f";

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorBasePosition());

        #region Inspector

        public override void Draw(Rect rect, TweenBase tween)
        {
            //EditorGUI.LabelField(rect, "       THIS IS BASE TWEEN");
        }

        #endregion
    }
}
