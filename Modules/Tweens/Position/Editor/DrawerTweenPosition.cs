using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public class DrawerTweenPosition : ITweenEditor
    {
        public string Name => "Position";

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            DrawerTween.Add<TweenPosition>(new DrawerTweenPosition());
        }

        public void OnGUI(Rect rect, Tween tween)
        {

        }
        
    }
}
