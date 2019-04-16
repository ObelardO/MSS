using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    /*
    [CustomPropertyDrawer(typeof(MSSTweenData))]
    public class MSSTweenDataRotationEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            // Draw label
            label.text += "_p";

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            //EditorGUI.PropertyField(position, property.FindPropertyRelative("tweenValue"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
    */
}
