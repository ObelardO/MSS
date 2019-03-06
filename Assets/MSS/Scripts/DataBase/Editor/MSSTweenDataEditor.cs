using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public class MSSTweenDataEditor
    {
        #region GUI

        public virtual void OnGUISpecifics(MSSTweenData tweenData)
        {

        }

        public static void OnGUI(MSSStateData stateData, MSSTweenData tweenData)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(tweenData.tweenName);
                if (GUILayout.Button("x")) MSSStateDataEditor.RemoveTweenData(tweenData, stateData);
            EditorGUILayout.EndHorizontal();

            if ((MSSTweenData)tweenData == null) return;

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

        /*
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
        */
    }
}
