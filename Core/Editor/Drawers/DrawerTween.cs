using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Assertions.Comparers;
using System;

namespace Obel.MSS.Editor
{
    [CustomPropertyDrawer(typeof(Tween))]
    internal class DrawerTween : PropertyDrawer
    {
        #region Properties



        #endregion

        #region Public methods



        #endregion

        #region Inspector

        public static void DrawBackground(Rect rect, int index, bool isActive, bool isFocused) => EditorGUI.DrawRect(rect, Color.clear);

        public static void Draw(Rect rect, Tween tween)
        {
            IGenericTweenEditor editor = EditorTween.Get(tween.GetType());

            if (editor == null)
            {
                EditorGUI.HelpBox(rect, "unknown tween module: \"" + tween.name + "\"", MessageType.Warning);
                return;
            }

            editor.OnGUI(rect, tween);
        }

        public static void DrawHeader(Rect rect) => EditorGUI.LabelField(rect, "Tweens");

        public static void DrawEmptyList(Rect rect) => EditorGUI.LabelField(rect, "Click + to add tween");

        public static float GetHeight<T>(T tween) where T : Tween => GetHeight(tween.GetType());

        public static float GetHeight(Type @Type)
        {
            IGenericTweenEditor editor = EditorTween.Get(@Type);

            return editor == null ? EditorGUIUtility.singleLineHeight : editor.Height;
        }

        #endregion
    }
}