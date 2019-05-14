using UnityEngine;
using UnityEditor;
using Obel.MSS;
using System;
using System.Reflection;
using UnityEngine.Events;

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

            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (tween is MSSTweenPosition) MSSTweenPositionEditor.OnGUI(tween as MSSTweenPosition);
            else if (tween is MSSTweenRotation) MSSTweenRotationEditor.OnGUI(tween as MSSTweenRotation);

            EditorGUILayout.EndVertical();

            GUILayout.Space(3);
        }

        public static void DrawHeader(MSSTween tween)
        {
            DrawHeader(tween, null);
        }

        public static void DrawHeader(MSSTween tween, UnityAction capture)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("{0} tween", tween.title));
                if (capture != null && GUILayout.Button("cv")) capture.Invoke();
                if (GUILayout.Button("x")) MSSStateEditor.RemoveTween(tween, currentState);
            EditorGUILayout.EndHorizontal();
        }

        /*
        public static void DrawValue<T>(IMSSTweenValue<T> tween) where T : struct
        {
            T tweenValue = tween.tweenValue;

            MSSEditorUtils.DrawGenericProperty(ref tweenValue);

            if (!tweenValue.Equals(tween.tweenValue))
            {
                Undo.RecordObject(tween as MSSTween, "[MSS] tween");
                tween.tweenValue = tweenValue;
            }
        }
        */

        #endregion


    }
}
