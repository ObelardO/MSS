using UnityEngine;
using UnityEditor;
using Obel.MSS.Editor;
using System.Reflection;

namespace Obel.MSS.Modules.Tweens.Editor
{
    internal class EditorTweenRotation : EditorGenericTween<TweenRotation, Transform, Vector3>
    {
        #region Properties

        public override string Name => "T/Rotation";
        public override float Height => EditorConfig.Sizes.SingleLine * 2;

        private static readonly GUIContent contentLocal = new GUIContent("Is local");
        private static readonly GUIContent contentMode = new GUIContent("Rotation Mode");

        #endregion

        #region Init

        [InitializeOnLoadMethod]
        private static void ApplicationStart() => EditorTween.Add(new EditorTweenRotation(), EditorGUI.Vector3Field, GUIContent.none);

        #endregion

        #region Inspector

        public override void Draw(Rect rect, TweenRotation tween)
        {
            rect.height = EditorConfig.Sizes.SingleLine;
            EditorLayout.PropertyField(rect, ref tween.IsLocal, EditorGUI.Toggle, InspectorStates.Record, contentLocal);

            rect.y += EditorConfig.Sizes.SingleLine;
            tween.Mode = (TweenRotation.RotationMode)EditorLayout.PropertyField(rect, tween.Mode, EditorGUI.EnumPopup, InspectorStates.Record, contentMode);
        }

        public override void Capture(TweenRotation tween)
        {
            if (tween.Mode == TweenRotation.RotationMode.Quaternion)
            {
                tween.Capture();
                return;
            }

            if (tween.IsLocal) tween.Value = TransformUtils.GetInspectorRotation(tween.State.Group.gameObject.transform);
            else tween.Value = tween.State.Group.gameObject.transform.eulerAngles;

            Debug.Log("Rotation tween capturing");
        }

        // Dirty way to get inspector rotation
        /*private static Vector3 GetInspectorRotation(Transform transform)
        {
            Vector3 result = Vector3.zero;
            MethodInfo mth = typeof(Transform).GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyInfo pi = typeof(Transform).GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
            object rotationOrder = null;
            if (pi != null)
            {
                rotationOrder = pi.GetValue(transform, null);  
            }
            if (mth != null)
            {
                object retVector3 = mth.Invoke(transform, new object[] { rotationOrder });
                result = (Vector3)retVector3;
            }

            return result;
        }
        */
        

        #endregion

    }
}
