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


        /*
        private static MSSTweenDataEditor _instance;
        public static MSSTweenDataEditor instance 
        {
            get
            {
                if (_instance == null) _instance = new MSSTweenDataEditor();
                return _instance;
            }
        }
        */

        /*
        public virtual void OnGUISpecifics(MSSTweenData tweenData)
        {

        }
        */
        /*
        public delegate void OnTweenGUI(MSSStateData stateData, MSSTweenData tweenData);
        public static event OnTweenGUI onTweenGUI;
        
        public virtual void OnTweenGUI(MSSStateData stateData, MSSTweenData tweenData)
        {
            EditorGUILayout.LabelField("TWEEN");
        }
        */

        public static void OnGUI(MSSState state, MSSTween tween)
        {
            //instance.OnTweenGUI(stateData, tweenData);


            //OnTweenGUI(stateData, tweenData);


            //EditorGUILayout.BeginHorizontal();

            // onTweenGUI(stateData, tweenData);

            //EditorGUILayout.LabelField(tweenData.tweenName);
            //if (GUILayout.Button("x")) MSSStateDataEditor.RemoveTweenData(tweenData, stateData);
            //EditorGUILayout.EndHorizontal();

            if ((MSSTween)tween == null) return;


            DrawProperty(tween, "_tweenValue");


            //OnGUISpecifics(tweenData);

            /*
            if (tweenData is MSSTweenDataPosition)
            {
                MSSEditorUtils.DrawMessageBox("POSITION", MessageType.Warning);
            }
            */

            /*
            if (tweenData is MSSTweenDataRotation)
            {
                MSSEditorUtils.DrawMessageBox("ROTATION", MessageType.Warning);
            }
            */
            /*

            SerializedObject serializedTween = new SerializedObject(tweenData);

            //serializedTween.

            SerializedProperty serializedProperty = serializedTween.FindProperty("_tweenValue");

            if (serializedProperty == null)
            {
                MSSEditorUtils.DrawMessageBox("invalid property", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.PropertyField(serializedProperty);
            }

            */

            //tweenData = 

            //DrawProperty(tweenData, "_tweenValue");

            //EditorGUI.BeginChangeCheck();
            //T tweenValue = (MSSTweenDataProxy)tweenData.tweenValue;

            /*
            Type tweenValueType = tweenData.tween
            */
            //var tweenValue = ((IMSSTweenDataValue<Vector3>)tweenData).tweenValue;

            //MSSEditorUtils.DrawGenericProperty(ref a);//, tweenData.tweenName);
            //if (EditorGUI.EndChangeCheck())
            //{
            //    Undo.RecordObject((MSSTweenData)tweenData, "[MSS][Tween] edit");
            //    tweenData.tweenValue = tweenValue;
            //}
        }

        #endregion

        
        #region Properties via reflections

        private delegate void SetValue<T>(T value);
        private delegate T GetValue<T>();

        public static void DrawProperty<T>(T obj, string propertyName)
        {
            if (!PropertyIsValid(obj, propertyName))
            {
                MSSEditorUtils.DrawMessageBox("invalid property: " + propertyName, MessageType.Warning);
                return;
            }

            MSSEditorUtils.DrawMessageBox("Valid!", MessageType.Info);
        }

        private static bool PropertyIsValid<T>(T obj, string propertyName)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);
            return property != null;
        }

        #endregion
        
    }
}
