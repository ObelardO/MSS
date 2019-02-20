using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{/*
    [CustomPropertyDrawer(typeof(MSSState))]
    public class MSSStateDrawer : PropertyDrawer
    {

        const float rows = 2;  // total number of rows

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * rows;// (property.isExpanded ? rows : 1);  // assuming original is one row
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("name"), GUIContent.none);

            position.y += position.height;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("tweens"), GUIContent.none);

            EditorGUI.EndProperty();


        }


    }
    */
}
