using System;
using System.Collections.Generic;
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

        #endregion

        #region Inspector

        public virtual void OnGUI(Rect rect, Tween tween) { OnGUI(rect, tween as T); }

        public virtual void OnGUI(Rect rect, T tween)
        {
            EditorGUI.HelpBox(rect, "no drawer for tween: \"" + Name + "\"", MessageType.Warning);
        }

        #endregion
    }
}
