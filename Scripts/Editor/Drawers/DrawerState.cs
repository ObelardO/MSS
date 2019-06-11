using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace Obel.MSS.Editor
{

    /*[CustomPropertyDrawer(typeof(State))]
    public class DrawerState : PropertyDrawer
    {
        private static GUIContent contentLabel = new GUIContent("Name"),
            delayLabel = new GUIContent("Delay"),
            durationLabel = new GUIContent("Duration"),
            testLabel = new GUIContent("Test");

        public static State drawingState;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            //StateEditorValues editorValues = GetStateEditorValues();

      



            EditorGUI.DrawRect(rect, Color.white * 0.4f);// BeginVertical(GUI.skin.box);


            Rect foldOutRect = new Rect(rect.x, rect.y, rect.width, 20);




            GUI.Box(foldOutRect, GUIContent.none, GUI.skin.box);

            rect.height = 20;

            rect.x += 15;
            rect.y += 2;
            rect.width -= 40;
  

            //editorValues.foldOut.target = EditorGUI.Foldout(rect, editorValues.foldOut.target,
                //new GUIContent(drawingState.name), true, GUI.skin.label);



            EditorGUI.BeginProperty(rect, label, property);

            SerializedProperty tweensProperty = property.FindPropertyRelative("tweens");
            

            Rect nameRect = new Rect(rect.x + 4, rect.y + 60, rect.width - 8, 16);
            //Rect delayRect = new Rect(rect.x + 4, rect.y + 22, rect.width - 8, 16);
            Rect durationRect = new Rect(rect.x + 4, rect.y + 40, rect.width - 8, 16);
            //Rect addButtonRect = new Rect(rect.x + 4, rect.y + 78 + 80 * tweensProperty.arraySize, rect.width - 8, 20);


            //EditorGUI.LabelField(durationRect, "> " + editorValues.foldOut.faded);

            
            //EditorGUI.DrawRect(new Rect(rect.x + 2, rect.y + 2, rect.width - 2, rect.height - 6), Color.white * 0.4f);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), contentLabel);
            /*
            EditorGUI.PropertyField(delayRect, property.FindPropertyRelative("delay"), delayLabel);
            EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("duration"), durationLabel);
            */
            //State stateKey = drawingState;

            // TODO


            //   editorValues.editing = EditorGUI.Toggle(addButtonRect, testLabel, editorValues.editing);


            // Debug.Log(statesDictionary.Count);

            /*
            for (int i = 0; i < tweensProperty.arraySize; i++)
            {
                DrawerTween.drawingTween = drawingState.tweens[i];

                EditorGUI.PropertyField(new Rect(rect.x + 4, rect.y + 58 + 80 * i, rect.width - 8, 80),
                    tweensProperty.GetArrayElementAtIndex(i));
            }

            if (GUI.Button(addButtonRect, "Add Tween"))
            {
                StatesBehaviour behaviour = property.serializedObject.targetObject as StatesBehaviour;
                Debug.Log(behaviour.name);

                drawingState.tweens.Add(new Tween());
            }
            */

    /*
            EditorGUI.EndProperty();
        }
        */

        /*
        private Dictionary<string, StateEditorValues> statesDictionary = new Dictionary<string, StateEditorValues>();

        private class StateEditorValues
        {
            public bool editing;
            public AnimBool foldOut;

            public StateEditorValues()
            {

                foldOut = new AnimBool(false);
                


                foldOut.valueChanged.AddListener(() => StatesBehaviourEditor.instance.Repaint());
            }

            //public bool 
        }

        private StateEditorValues GetStateEditorValues()
        {
            StateEditorValues editorValues;

            if (statesDictionary.ContainsKey(drawingState.name))
            {
                editorValues = statesDictionary[drawingState.name];
            }
            else
            {
                editorValues = new StateEditorValues();
                statesDictionary.Add(drawingState.name, editorValues);
            }

            return editorValues; 
        }
        */
    //}
}