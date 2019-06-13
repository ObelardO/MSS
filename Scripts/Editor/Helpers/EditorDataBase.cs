using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public static class EditorDataBase
    {
        private static string AssetPath = "Assets/MSS/DataBase-DONT-DELETE-THIS.asset";

        public static DataBase instance
        {
            get
            {
                if (DataBase.instance == null) DataBase.instance = LoadDataBaseAsset();
                if (DataBase.instance == null) DataBase.instance = CreateDataBaseAsset();

                return DataBase.instance;
            }
        }

        private static DataBase CreateDataBaseAsset()
        {
            DataBase dataBase = ScriptableObject.CreateInstance<DataBase>();
            AssetDatabase.CreateAsset(dataBase, AssetPath);
            AssetDatabase.SaveAssets();

            return dataBase;
        }

        private static DataBase LoadDataBaseAsset()
        {
            return AssetDatabase.LoadAssetAtPath(AssetPath, typeof(DataBase)) as DataBase;
        }

        public static T SaveAsset<T>() where T : DBCollectionItem
        {
            return SaveAsset<T>(null, typeof(T).ToString());
        }

        public static T SaveAsset<T>(Action<T> instancedCallback = null, string assetName = null) where T : DBCollectionItem
        {
            T newAsset = ScriptableObject.CreateInstance<T>();

            if (assetName != null) newAsset.name = assetName;

            Undo.RegisterCreatedObjectUndo(newAsset, "[MSS] saving asset");

            AssetDatabase.AddObjectToAsset(newAsset, instance);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (instancedCallback != null) instancedCallback(newAsset);

            return newAsset;
        }

        public static T RemoveAsset<T>(T removingAsset, bool useRecording = true) where T : DBCollectionItem
        {
            if (useRecording)
                Undo.DestroyObjectImmediate(removingAsset);
            else
                UnityEngine.Object.DestroyImmediate(removingAsset, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return removingAsset;
        }
    }

}
