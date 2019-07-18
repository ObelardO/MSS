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
            DrawerTween.AddTweenEditor(new DrawerTweenPosition());
        }

        public DrawerTweenPosition()
        {

        }

        public void OnGUI()
        {

        }

        public void OnAddButton()
        {
            DrawerTween.OnAddTween<TweenPosition>();
        }
    }
}
