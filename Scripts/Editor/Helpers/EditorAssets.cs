using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS.Editor
{
    using Object = UnityEngine.Object;

    internal static class EditorAssets
    {
        public static bool autoFocusOnCreatedAsset = false;

        public static T Create<T>(string assetName) where T : CollectionItem
        {
            T newAsset = ScriptableObject.CreateInstance<T>();

            Undo.RegisterCreatedObjectUndo(newAsset, "[MSS] creating asset");

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
            T newAsset = ScriptableObject.CreateInstance<T>();

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
    }

}
