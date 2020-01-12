using UnityEngine;
using UnityEditor;
using Obel.MSS.Editor;
using System;

namespace Obel.MSS.Modules.Tweens.Editor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EditorTweenAttribute : Attribute
    {
        public readonly Type tweenType;
        public readonly Type componentType;
        public readonly Type valueType;

        /// <summary>
        /// Creates a new attribute.
        /// </summary>
        /// <param name="attributeType">The type that this decorator can inspect</param>
        public EditorTweenAttribute(Type tweenType, Type componentType, Type valueType)
        {
            this.tweenType = tweenType;
            this.componentType = componentType;
            this.valueType = valueType;
        }
    }


    [EditorTween(typeof(TweenBase), typeof(Transform), typeof(Vector3))]
    internal class EditorTweenBase : EditorGenericTween<TweenBase, Transform, Vector3>
    {
        #region Properties

        public override string Name => "T/Base";
        public override string DisplayName => "Empty tween";
        public override bool IsMultiple => true;

        #endregion

        #region Init

        //[InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenBase());

        #endregion
    }
}