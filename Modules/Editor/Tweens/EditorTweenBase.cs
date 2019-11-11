using UnityEngine;
using UnityEditor;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenBase : EditorGenericTween<TweenBase, Transform, Vector3>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "Empty tween";
        public override bool IsMultiple => true;

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenBase());

        #endregion
    }
}