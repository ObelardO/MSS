/*
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Obel.MSS.Editor
{
    internal static class EditorAssets
    {
        #region Properties

        public static bool autoFocusOnCreatedAsset = false;

        #endregion

        #region Public methods

        public static T Create<T>(string assetName) where T : CollectionItem
        {
            T newAsset = ScriptableObject.CreateInstance<T>();

            //Undo.RegisterCreatedObjectUndo(newAsset, "[MSS] creating asset");

            string path = string.Empty;
            if (Selection.activeObject != null) path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!Directory.Exists(path)) path = "Assets";

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, assetName + ".asset"));

            AssetDatabase.CreateAsset(newAsset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (autoFocusOnCreatedAsset)
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = newAsset;
            }

            return newAsset;
        }

        public static T Save<T>(Object rootAsset, string assetName = null, Action <T> instancedCallback = null) where T : CollectionItem
        {
            if (rootAsset == null)
            {
                Debug.Log("NULL ROOT");
                return null;
            }

            T newAsset = ScriptableObject.CreateInstance<T>();

            if (newAsset == null)
            {
                Debug.Log("NULL ASSET");
                return null;
            }

            newAsset.name = assetName ?? rootAsset.name;

            Undo.RegisterCreatedObjectUndo(newAsset, "[MSS] saving asset");

            AssetDatabase.AddObjectToAsset(newAsset, rootAsset);
            Refresh(rootAsset);
            AssetDatabase.Refresh();

            if (instancedCallback != null) instancedCallback(newAsset);

            return newAsset;
        }

        public static T Remove<T>(T removingAsset, bool useRecording = true) where T : CollectionItem
        {
            if (useRecording)
                Undo.DestroyObjectImmediate(removingAsset);
            else
                Object.DestroyImmediate(removingAsset, true);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return removingAsset;
        }

        public static void Refresh(Object assetObject)
        {
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(assetObject));
        }

        public static T AddItem<T>(Collection<T> root, string name = "[MSS]") where T : CollectionItem
        {
            T newItem = (T)Activator.CreateInstance(typeof(T));// Save<T>(root, name);
            root.Add(newItem);
            return newItem;
        }
    
        #endregion
    }
}
*/