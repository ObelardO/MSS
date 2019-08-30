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

        /*
        public void ApplyValue(dynamic value, TweenPosition tween)
        {
            tween.Value = value;
        }

        public dynamic DrawValueProperty(Rect rect, TweenPosition tween)
        {
            return EditorGUI.Vector3Field(rect, string.Empty, tween.Value);
        }
        */

        public override void Draw(Rect rect, TweenPosition tween)
        {
            tween.Value = EditorGUI.Vector3Field(rect, string.Empty, tween.Value);
        }

        #endregion
    }
}
