using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEditorInternal;
using System;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(Tween))]
    public class DrawerTween : PropertyDrawer
    {
        #region Properties

        private static readonly List<ITweenEditor> tweens = new List<ITweenEditor>();

        private static GenericMenu tweensMenu = new GenericMenu();
        private static State selectedState;

        #endregion

        public static void Add<T>(ITweenEditor tweenEditor) where T : Tween
        {
            if (!tweens.Contains(tweenEditor)) tweens.Add(tweenEditor);

            tweensMenu.AddItem(new GUIContent(tweenEditor.Name), false, () =>
            {
                EditorActions.Add(() => EditorAssets.AddItem(selectedState, string.Concat("[Tween] ", tweenEditor.Name)), selectedState);
            });
        }

        #region Inspector

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        public static void Draw(Rect rect, int index, bool isActive, bool isFocused)
        {           
            EditorGUI.LabelField(rect, DrawerState.editorValues.state[index].name.Replace("[Tween] ", string.Empty));
        }

        public static void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Tweens");
        }

        public static void DrawEmptyList(Rect rect)
        {
            EditorGUI.LabelField(rect, "Click + to add tween");
        }

        #endregion

        #region Inspector callbacks

        public static void OnAddButton(ReorderableList list)
        {
            selectedState = DrawerState.editorValues.state;
            tweensMenu.ShowAsContext();
        }

        public static void OnRemoveButton(ReorderableList list)
        {
            State drawingState = DrawerState.editorValues.state;

            EditorActions.Add(() =>
            {
                Tween removingTween = drawingState.items[list.index];

                drawingState.Remove(removingTween, false);
                EditorAssets.Remove(removingTween);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(drawingState));
            },
            drawingState);
        }

        #endregion
    }
}