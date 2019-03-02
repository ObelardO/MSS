﻿using System.Collections;
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
    [CustomEditor(typeof(MSSDataBase))]
    public class MSSDataBaseEditor : UnityEditor.Editor
    {
        private static string AssetPath = "Assets/MSSDataBase-DONT-DELETE-THIS.asset";

        private static MSSDataBase _instance;
        public static MSSDataBase instance
        {
            get
            {
                if (_instance == null)
                    _instance = LoadDataBaseAsset();

                if (_instance == null)
                    _instance = CreateDataBaseAsset();

                return _instance;
            }
        }

        #region Collection editor

        public static void AddStateGroupsData(int objectID)
        {
            Undo.RecordObject(MSSDataBaseEditor.instance, "[MSS] Add group");

            MSSStateGroupData newStateGroupData = MSSDataBaseEditor.SaveAsset<MSSStateGroupData>(StateGroupsDataInstanced, "[MSS][Group]");
            newStateGroupData.objectID = objectID;

            MSSDataBaseEditor.instance.stateGroupsData.Add(newStateGroupData);
        }

        private static void StateGroupsDataInstanced(MSSStateGroupData stateGroupData)
        {

        }

        public static void RemoveStateGroupsData(MSSStateGroupData stateGroupData)
        {
            Undo.RecordObject(MSSDataBaseEditor.instance, "[MSS] Remove a state");
            MSSDataBaseEditor.instance.stateGroupsData.Remove(stateGroupData);
            MSSStateGroupDataEditor.RemoveStatesData(stateGroupData);
            MSSDataBaseEditor.RemoveAsset(stateGroupData);
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            OnGUI();
        }

        public static void OnGUI()
        {
            if (instance == null)
            {
                Debug.LogError("[MSS] can't find MSS DataBase File!");
                return;
            }

            instance.stateGroupsData.ToList().ForEach(stateGroupData => MSSStateGroupDataEditor.OnGUI(stateGroupData));

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Group")) AddStateGroupsData((int)(Time.time * 1000));
        }

        #endregion

        #region Work with Assets 

        private static MSSDataBase CreateDataBaseAsset()
        {
            MSSDataBase mssDataBase = CreateInstance<MSSDataBase>();
            AssetDatabase.CreateAsset(mssDataBase, AssetPath);
            AssetDatabase.SaveAssets();

            return mssDataBase;
        }

        private static MSSDataBase LoadDataBaseAsset()
        {
            MSSDataBase mssDataBase = AssetDatabase.LoadAssetAtPath(AssetPath, typeof(MSSDataBase)) as MSSDataBase;

            return mssDataBase;
        }

        public static T SaveAsset<T>(Action<T> instancedCallback = null, string assetName = null) where T : ScriptableObject
        {
            T newAsset = ScriptableObject.CreateInstance<T>();

            if (instancedCallback != null) instancedCallback(newAsset);
            if (assetName != null) newAsset.name = assetName;

            Undo.RegisterCreatedObjectUndo(newAsset, "[MSS] New object");

            AssetDatabase.AddObjectToAsset(newAsset, instance);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return newAsset;
        }

        public static T RemoveAsset<T>(T removingAsset, bool useRecording = true) where T : ScriptableObject
        {
            if (useRecording)
                Undo.DestroyObjectImmediate(removingAsset);
            else
                DestroyImmediate(removingAsset, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return removingAsset;
        }

        #endregion
    }

}
