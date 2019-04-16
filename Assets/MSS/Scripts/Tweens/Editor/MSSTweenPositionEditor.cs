using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    
    /*
    public class MSSTweenDataPositionEditor// : MSSTweenDataEditor
    {

        public ov*erride void OnTweenGUI(MSSStateData stateData, MSSTweenData tweenData)
        {
            EditorGUILayout.LabelField("POSITION TWEEN");
        }

    }
*/

    /*
    [CustomPropertyDrawer(typeof(MSSTweenDataPosition))]
    public class MSSTweenDataPositionEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            label.text = "huiak";



            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            //EditorGUI.PropertyField(position, property.serializedObject.FindProperty("_tweenValue"), GUIContent.none);

            EditorGUI.EndProperty();
            
        }
    }
    
    */
}
