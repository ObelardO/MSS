using UnityEngine;
using UnityEditor;
using Obel.MSS;
using System;
using System.Reflection;

namespace Obel.MSS.Editor
{
    public class MSSTweenEditor
    {
        #region GUI

        private static MSSState currentState;

        public static void OnGUI(MSSState state, MSSTween tween)
        {
            currentState = state;

            if (tween == null) return;

            if (tween is MSSTweenPosition) MSSTweenPositionEditor.OnGUI(tween as MSSTweenPosition);
            else if (tween is MSSTweenRotation) MSSTweenRotationEditor.OnGUI(tween as MSSTweenRotation);
        }

        public static void DrawHeader(MSSTween tween)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("{0} tween", tween.title));
                if (GUILayout.Button("x")) MSSStateEditor.RemoveTween(tween, currentState);
            EditorGUILayout.EndHorizontal();

        }

        #endregion


    }
}
