using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(Tween))]
    internal class DrawerTween : PropertyDrawer
    {
        #region Properties

        private static readonly List<ITweenEditor> tweensEditors = new List<ITweenEditor>();
        private static GenericMenu tweensMenu = new GenericMenu();
        private static State selectedState;

        #endregion

        #region Public methods

        public static void Add<T>(EditorTween<T> tweenEditor) where T : Tween
        {
            if (!tweensEditors.Contains(tweenEditor))
            {
                tweensEditors.Add(tweenEditor);
                tweenEditor.TweenType = typeof(T);
            }

            tweensMenu.AddItem(new GUIContent(tweenEditor.Name), false, () =>
            {
                EditorActions.Add(() =>
                {
                    T tween = EditorAssets.Save<T>(selectedState, string.Concat("[Tween] ", tweenEditor.Name));
                    selectedState.Add(tween);

                }, selectedState);
            });
        }

        #endregion

        #region Inspector

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        public static void Draw(Rect rect, int index, bool isActive, bool isFocused)
        {            
            foreach (ITweenEditor tweenEditor in tweensEditors)
            {
                if (tweenEditor.TweenType.Equals(DrawerState.editorValues.state[index].GetType()))
                {
                    tweenEditor.OnGUI(rect, DrawerState.editorValues.state[index]);
                    return;
                }
            }

            EditorGUI.HelpBox(rect, "unknown tween module: \"" + DrawerState.editorValues.state[index].name + "\"", MessageType.Warning);
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