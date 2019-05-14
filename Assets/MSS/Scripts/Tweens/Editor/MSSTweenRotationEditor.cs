using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public static class MSSTweenRotationEditor
    {
        #region GUI

        public static void OnGUI(MSSTweenRotation tween)
        {
            MSSTweenEditor.DrawHeader(tween, 
                () => tween.tweenValue = MSSEditorUtils.GetInspectorRotation(MSSItemEditor.item.transform));

            MSSEditorUtils.DrawGenericProperty(ref tween.tweenValue, tween);
            MSSEditorUtils.DrawGenericProperty(ref tween.rotationMode, "Rotation mode", tween);
        }

        #endregion
    }
}