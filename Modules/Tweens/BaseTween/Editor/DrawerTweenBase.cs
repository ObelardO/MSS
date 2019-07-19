using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public class DrawerTweenBase : ITweenEditor
    {
        public string Name => "Base";

        [InitializeOnLoadMethod]
        public static void ApplicationStart()
        {
            DrawerTween.Add<TweenBase>(new DrawerTweenBase());
        }

        public void OnGUI(Rect rect, Tween tween)
        {

        }
    }
}
