using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class DrawerTweenPosition : EditorTween<TweenPosition>
    {
        public override string Name => "T/Position";

        public override float Height => 50f;

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            DrawerTween.Add(new DrawerTweenPosition());
        }

        public override void OnGUI(Rect rect, TweenPosition tween)
        {
            EditorGUI.LabelField(rect, "THIS IS POSITION TWEEN");
        }
    }
}
