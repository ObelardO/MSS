using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomEditor(typeof(StatesGroup))]
    public class InspectorStatesGroup : UnityEditor.Editor
    {
        #region Properties

        private StatesGroup statesGroup;
        //private SerializedObject serializedStatesGroup;
        private SerializedProperty statesGroupProperty;
        //private ReorderableList statesReorderableList;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            if (!(target is StatesGroup)) return;

            statesGroup = (StatesGroup)target;


            //statesGroupProperty = new SerializedObject(serializedObject.FindProperty("statesGroup").objectReferenceValue);

       
        }

        private void OnDisable()
        {

        }

        #endregion

        #region Inspector

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            //serializedStatesGroup.Update();

            //serializedStatesGroup.ApplyModifiedProperties();
        }

        #endregion
    }
}