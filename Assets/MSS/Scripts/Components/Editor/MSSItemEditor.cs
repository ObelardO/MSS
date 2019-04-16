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
    [CustomEditor(typeof(MSSItem))]
    public class MSSItemEditor : UnityEditor.Editor
    {
        private MSSItem item;

        private void OnEnable()
        {
            item = (MSSItem)target;

            GetStateGroup();
        }

        private void GetStateGroup()
        {
            int objectID = item.gameObject.GetInstanceID();

            if (item.stateGroup == null || item.stateGroup.objectID != objectID)
            {
                MSSBaseEditor.AddStateGroupsData(objectID);
                item.stateGroup = MSSBaseEditor.GetStateGroupData(objectID);
            }
        }

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            item.dataBaseID = EditorGUILayout.IntField("id", item.dataBaseID);

            if (item.stateGroup == null)
            {
                Debug.Log("NUUUL!");
                GetStateGroup();
            }
            else
            {
                MSSStateGroupEditor.OnGUI(item.stateGroup);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
