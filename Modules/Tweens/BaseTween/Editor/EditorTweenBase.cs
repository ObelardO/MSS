using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorBasePosition : EditorGenericTween<TweenBase>
    {
        public override string Name => "T/Base";

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            EditorTween.Add(new EditorBasePosition()); 
            
        }

        public override void Draw(Rect rect, TweenBase tween)
        {
            EditorGUI.LabelField(rect, "THIS IS BASE TWEEN");
        }
    }
}
