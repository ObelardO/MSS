using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class DrawerBasePosition : EditorGenericTween<TweenBase>
    {
        public override string Name => "T/Base";

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            EditorTween.Add(new DrawerBasePosition()); 
            
        }

        public override void OnGUI(Rect rect, TweenBase tween)
        {
            EditorGUI.LabelField(rect, "THIS IS BASE TWEEN");
        }
    }
}
