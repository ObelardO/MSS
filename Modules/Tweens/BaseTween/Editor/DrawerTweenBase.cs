using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class DrawerBasePosition : EditorTween<TweenBase>
    {
        public override string Name => "Base";

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            DrawerTween.Add(new DrawerBasePosition()); 
            
        }

        public override void OnGUI(Rect rect, TweenBase tween)
        {
            EditorGUI.LabelField(rect, "THIS IS BASE TWEEN");
        }
    }
}
