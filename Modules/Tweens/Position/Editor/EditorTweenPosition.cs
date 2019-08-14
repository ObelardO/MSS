using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorTweenPosition : EditorGenericTween<TweenPosition>
    {
        public override string Name => "T/Position";

        public override float Height => 50;

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            EditorTween.Add(new EditorTweenPosition());
        }

        public override void Draw(Rect rect, TweenPosition tween)
        {
            EditorGUI.LabelField(rect, "THIS IS POSITION TWEEN");
        }
    }
}
