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
    [CustomEditor(typeof(MSSBase))]
    public class MSSBaseEditor : UnityEditor.Editor
    {
        private static string AssetPath = "Assets/MSSDataBase-DONT-DELETE-THIS.asset";

        
        public static MSSBase instance
        {
            get
            {
                if (MSSBase.instance == null) MSSBase.instance = LoadDataBaseAsset();

                if (MSSBase.instance == null) MSSBase.instance = CreateDataBaseAsset();

                return MSSBase.instance;
            }
        }

        #region Collection editor

        public static void AddStateGroupsData(int objectID)
        {
            Undo.RecordObject(MSSBaseEditor.instance, "[MSS] Add group");

            MSSStateGroup newStateGroup = MSSBaseEditor.SaveAsset<MSSStateGroup>(StateGroupsInstanced, "[MSS][Group]");
            newStateGroup.objectID = objectID;

            instance.Add(newStateGroup);

            MSSStateGroupEditor.AddState(newStateGroup);
            MSSStateGroupEditor.AddState(newStateGroup);

            instance.Last[0].stateName = "closed";
            instance.Last[1].stateName = "opened";
        }

        private static void StateGroupsInstanced(MSSStateGroup stateGroup)
        {

        }

        public static void RemoveStateGroups(MSSStateGroup stateGroup)
        {
            MSSStateGroupEditor.RemoveStates(stateGroup);

            Undo.RecordObject(MSSBaseEditor.instance, "[MSS] Remove a state");

            instance.Remove(stateGroup, false);
            MSSBaseEditor.RemoveAsset(stateGroup);
        }

        public static MSSStateGroup GetStateGroupData(int objectID)
        {
            return instance.Find(objectID);
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

            instance.ForEach(stateGroupData => MSSStateGroupEditor.OnGUI(stateGroupData));

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Group")) AddStateGroupsData((int)(Time.time * 1000));
        }

        #endregion

        #region Work with Assets 

        private static MSSBase CreateDataBaseAsset()
        {
            MSSBase mssDataBase = ScriptableObject.CreateInstance<MSSBase>();
            AssetDatabase.CreateAsset(mssDataBase, AssetPath);
            AssetDatabase.SaveAssets();

            return mssDataBase;
        }

        private static MSSBase LoadDataBaseAsset()
        {
            MSSBase mssDataBase = AssetDatabase.LoadAssetAtPath(AssetPath, typeof(MSSBase)) as MSSBase;

            return mssDataBase;
        }

        public static T SaveAsset<T>(Action<T> instancedCallback = null, string assetName = null) where T : MSSCollectionItem
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

        public static T RemoveAsset<T>(T removingAsset, bool useRecording = true) where T : MSSCollectionItem
        {
            if (useRecording)
                Undo.DestroyObjectImmediate(removingAsset);
            else
                UnityEngine.Object.DestroyImmediate(removingAsset, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return removingAsset;
        }

        #endregion
    }

}
