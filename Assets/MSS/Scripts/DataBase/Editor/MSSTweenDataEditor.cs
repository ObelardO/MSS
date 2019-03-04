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

        public static void OnGUI(MSSStateData stateData, MSSTweenData tweenData)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(tweenData.tweenName);
                if (GUILayout.Button("x")) MSSStateDataEditor.RemoveTweenData(tweenData, stateData);
            EditorGUILayout.EndHorizontal();

            if ((MSSTweenData)tweenData == null) return;

            //EditorGUI.BeginChangeCheck();
            //T tweenValue = (MSSTweenDataProxy)tweenData.tweenValue;
            Vector3 a = Vector3.zero;
            MSSEditorUtils.DrawGenericProperty(ref a);//, tweenData.tweenName);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    Undo.RecordObject((MSSTweenData)tweenData, "[MSS][Tween] edit");
            //    tweenData.tweenValue = tweenValue;
            //}
        }

        #endregion
    }
}
