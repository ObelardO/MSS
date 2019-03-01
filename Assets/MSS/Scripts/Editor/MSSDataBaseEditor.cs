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


        public static void AddState()
        {
            Undo.RecordObject(instance, "[MSS] Add a new state");
            
            instance.statesData.Add(SaveAsset<MSSStateData>(StateDataInstanced, "[MSS][State]"));
        }

        private static void StateDataInstanced(MSSStateData stateData)
        {
            stateData.name = stateData.stateName;
        }

        public static void RemoveStateData(MSSStateData state)
        {
            Undo.RecordObject(instance, "[MSS] Remove a state");

            MSSStateDataEditor.RemoveTweensData(state);

            instance.statesData.Remove(state);
            RemoveAsset(state);
        }

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

            instance.statesData.ToList().ForEach(s => MSSStateDataEditor.OnGUI(s));

            EditorGUILayout.Space();

            if (GUILayout.Button("Add State")) AddState();
        }

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
    }

}
