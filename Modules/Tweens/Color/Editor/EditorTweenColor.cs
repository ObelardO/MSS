using System;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorTweenColor : EditorGenericTween<TweenColor, Color>
    {
        #region Properties

        public override string Name => "Color";
        public override bool Multiple => true;

        #endregion

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenColor(), EditorGUI.ColorField);

        #region Inspector


        public override void Draw(Rect rect, TweenColor tween)
        {
            //DrawValue(tween, rect, EditorGUI.Vector3Field);
        }

        #endregion
    }
}
