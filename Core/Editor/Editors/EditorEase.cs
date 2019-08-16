using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public static class EditorEase
    {
        public static void Draw(Rect rect, string easeName)
        {
            GUI.Button(rect, easeName, EditorStyles.popup);
        }
    }
}

