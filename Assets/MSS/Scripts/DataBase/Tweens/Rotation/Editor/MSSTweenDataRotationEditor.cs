using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(MSSTweenData))]
    public class MSSTweenDataEditorA : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(":)"));

            EditorGUI.EndProperty();
        }
    }


    [CustomPropertyDrawer(typeof(MSSTweenDataRotation))]
    public class MSSTweenDataRotationEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.PropertyField(position, property.FindPropertyRelative("tweenValue"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
