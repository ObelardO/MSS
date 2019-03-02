using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Obel.MSS;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;
using System.Linq;
using System;

namespace Obel.MSS.Editor
{
    public class MSSTweenDataEditor : UnityEditor.Editor
    {
        #region GUI

        public static void OnGUI<T>(MSSStateData stateData, MSSTweenData tweenData, T tweenValue)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(tweenData.tweenName);
                if (GUILayout.Button("x")) MSSStateDataEditor.RemoveTweenData(tweenData, stateData);
            EditorGUILayout.EndHorizontal();

            MSSEditorUtils.DrawGenericProperty(ref tweenValue, tweenData);
        }

        #endregion
    }
}
