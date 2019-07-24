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

        private static EditorState StateEditor => EditorState.Selected;

        #endregion

        #region Public methods



        #endregion

        #region Inspector

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.DrawRect(rect, Color.clear);
        }

        public static void Draw(Rect rect, int index, bool isActive, bool isFocused)
        {
            IGenericTweenEditor editor = EditorTween.Get(StateEditor.state[index].GetType());

            if (editor == null)
            {
                EditorGUI.HelpBox(rect, "unknown tween module: \"" + StateEditor.state[index].name + "\"", MessageType.Warning);
                return;
            }

            editor.OnGUI(rect, StateEditor.state[index]);
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
            IGenericTweenEditor editor = EditorTween.Get(StateEditor.state[index].GetType());

            float height = editor == null ? EditorGUIUtility.singleLineHeight : editor.Height;

            StateEditor.tweensListHeight += height;

            return height;
        }

        #endregion
    }
}