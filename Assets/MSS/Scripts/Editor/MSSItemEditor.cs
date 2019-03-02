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
            item = target as MSSItem;
        }

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            item.dataBaseID = EditorGUILayout.IntField("id", item.dataBaseID);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
