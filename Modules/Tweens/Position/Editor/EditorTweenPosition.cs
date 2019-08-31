using System;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorTweenPosition : EditorGenericTween<TweenPosition>
    {
        #region Properties

        public override string Name => "T/Position";
        public override float Height => 50;

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart()
        {
            EditorTween.Add(new EditorTweenPosition());
        }

        #region Inspector

        public override void Draw(Rect rect, TweenPosition tween)
        {
            DrawValue(tween, rect, EditorGUI.Vector3Field);
        }

        #endregion
    }
}
