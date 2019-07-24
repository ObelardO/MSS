using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Assertions.Comparers;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(Tween))]
    internal class DrawerTween : PropertyDrawer
    {
        #region Properties

        private static readonly List<ITweenEditor> tweensEditors = new List<ITweenEditor>();
        //private static GenericMenu tweensMenu = new GenericMenu();
        private static State selectedState;
        private static ITweenEditor drawingEditor;

        //public static float ListHeight { private set; get; }

        #endregion

        #region Public methods

        public static void Add<T>(EditorTween<T> tweenEditor) where T : Tween
        {
            if (!tweensEditors.Contains(tweenEditor))
            {
                tweensEditors.Add(tweenEditor);
                tweenEditor.TweenType = typeof(T);
                tweenEditor.AddAction = () =>
                {
                    EditorActions.Add(() =>
                    {
                        T tween = EditorAssets.Save<T>(selectedState, string.Concat("[Tween] ", tweenEditor.Name));
                        selectedState.Add(tween);

                    }, selectedState);
                };

            }
        }

        #endregion

        #region Inspector

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        public static void Draw(Rect rect, int index, bool isActive, bool isFocused)
        {

            /*
            foreach (ITweenEditor tweenEditor in tweensEditors)
            {
                if (tweenEditor.TweenType.Equals(DrawerState.editorValues.state[index].GetType()))
                {
                    drawingEditor = 
                    tweenEditor.OnGUI(rect, DrawerState.editorValues.state[index]);
                    return;
                }
            }
            */

            ITweenEditor drawingEditor = tweensEditors
                .Where(t => t.TweenType.Equals(DrawerState.editorValues.state[index].GetType())).FirstOrDefault();
 


            if (drawingEditor == null)
            {
                EditorGUI.HelpBox(rect, "unknown tween module: \"" + DrawerState.editorValues.state[index].name + "\"", MessageType.Warning);
                return;
            }

            drawingEditor.OnGUI(rect, DrawerState.editorValues.state[index]);
        }

        public static void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Tweens");
        }

        public static void DrawEmptyList(Rect rect)
        {
            EditorGUI.LabelField(rect, "Click + to add tween");
        }

        public static float GetHeight(int index)
        {
            ITweenEditor drawingEditor = tweensEditors
                .Where(t => t.TweenType.Equals(DrawerState.editorValues.state[index].GetType())).FirstOrDefault();

            float height = drawingEditor == null ? EditorGUIUtility.singleLineHeight : drawingEditor.Height;

            DrawerState.editorValues.tweensListHeight += height;

            return height;
        }

        #endregion

        #region Inspector callbacks

        public static void OnAddButton(ReorderableList list)
        {
            selectedState = DrawerState.editorValues.state;

            GenericMenu tweensMenu = new GenericMenu();

            foreach (ITweenEditor tweenEditor in tweensEditors)
            {
                if (selectedState.items.Where(t => t.GetType() == tweenEditor.TweenType).Count() == 0)
                    tweensMenu.AddItem(new GUIContent(tweenEditor.Name), false, () => tweenEditor.AddAction());
            }

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