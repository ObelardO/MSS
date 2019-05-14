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
        public MSSItem item;
        public static MSSItem sharedItem;

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
                item.stateGroup = MSSBaseEditor.GetStateGroupData(objectID);
                if (item.stateGroup != null) return;

                MSSBaseEditor.AddStateGroupsData(objectID);
                item.stateGroup = MSSBaseEditor.GetStateGroupData(objectID);
            }
        }

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            item.dataBaseID = EditorGUILayout.IntField("id", item.dataBaseID);
            if (EditorGUI.EndChangeCheck())
            {
                item.stateGroup = null;
                GetStateGroup();
            }

            if (item.stateGroup == null)
            {
                Debug.Log("NUUUL!");
                GetStateGroup();
                return;
            }
            else
            {
                sharedItem = item;

                MSSStateGroupEditor.OnGUI(item.stateGroup);

                sharedItem = null;
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
