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
            DrawerTween.AddTweenEditor(new DrawerTweenBase());
        }

        public DrawerTweenBase()
        {

        }

        public void OnGUI()
        {

        }

        public void OnAddButton()
        {
            DrawerTween.OnAddTween<TweenBase>();
        }
    }
}
