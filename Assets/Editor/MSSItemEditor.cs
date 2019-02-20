using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Obel.MSS;

namespace Obel.MSS.Editor
{

    [CustomEditor(typeof(MSSItem))]
    public class MSSItemEditor : UnityEditor.Editor
    {
        #region Content

        private static readonly GUIContent statesHolderTitle = new GUIContent("States");

        #endregion

        #region Private

        private ReorderableList itemStatesList;

        #endregion

        #region InspectorGUI

        public void OnEnable()
        {
            itemStatesList = new ReorderableList(serializedObject, serializedObject.FindProperty("states"), true, true, true, true);


            itemStatesList.drawElementCallback += (Rect rect, int index, bool selected, bool focused) =>
            {
                SerializedProperty property = itemStatesList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, property, GUIContent.none);
            };
            itemStatesList.drawHeaderCallback += rect =>
            {
                EditorGUI.LabelField(rect, statesHolderTitle);
            };
            itemStatesList.elementHeightCallback = (int index) =>
            {
                float height = 60;



                return height;
            };





        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();

            itemStatesList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(target);

            DrawDefaultInspector();
        }




        #endregion
    }
}
