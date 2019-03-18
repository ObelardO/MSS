using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(IMSSTweenDataValue<>))]
    public class MSSTweenDataPositionEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Debug.Log(2);

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.PropertyField(position, property.FindPropertyRelative("tweenValue"), GUIContent.none);
            EditorGUI.EndProperty();

            /*
            EditorGUI.BeginProperty(position, label, property);
            // Draw label

            Debug.Log(2);

            label.text += "_Vector3";
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);



            //EditorGUI.PropertyField(position, property.FindPropertyRelative("tweenValue"), label);

            EditorGUI.EndProperty();
            */
        }
    }
}
