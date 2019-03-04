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

            int objectID = item.gameObject.GetInstanceID();

            if (item.stateGroupData == null || item.stateGroupData.objectID != objectID)
            {
                MSSDataBaseEditor.AddStateGroupsData(objectID);
                item.stateGroupData = MSSDataBaseEditor.GetStateGroupData(objectID);
            }
        }

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            item.dataBaseID = EditorGUILayout.IntField("id", item.dataBaseID);

            if (item.stateGroupData == null)
            {
                Debug.Log("NUUUL!");
            }
            else
            {
                MSSStateGroupDataEditor.OnGUI(item.stateGroupData);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
