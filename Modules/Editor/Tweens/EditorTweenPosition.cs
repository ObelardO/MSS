using UnityEngine;
using UnityEditor;
using Obel.MSS.Editor;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenPosition : EditorGenericTween<TweenPosition, Transform, Vector3>
    {
        #region Properties

        public override string Name => "T/Position";

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenPosition(), EditorGUI.Vector3Field, GUIContent.none);

        #endregion
    }
}
