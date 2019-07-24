using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal class EditorTween<T> : ITweenEditor where T : Tween
    {
        #region Properties

        public virtual string Name { get; }

        public virtual Type TweenType { get; set; }

        public Action AddAction { get; set; }

        private float s_Height = EditorGUIUtility.singleLineHeight;
        public virtual float Height {
            get => s_Height;
            set => s_Height = value;
        }

        #endregion

        #region Inspector

        public virtual void OnGUI(Rect rect, Tween tween) { OnGUI(rect, tween as T); }

        public virtual void OnGUI(Rect rect, T tween)
        {
            EditorGUI.HelpBox(rect, "no drawer for tween: \"" + Name + "\"", MessageType.Warning);
        }

        #endregion

        public static ITweenEditor Get(List<ITweenEditor> editors)
        {
            return editors.Where(t => t.TweenType.Equals(typeof(T))).FirstOrDefault();
        }
    }
}
