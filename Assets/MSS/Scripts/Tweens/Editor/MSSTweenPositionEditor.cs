using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public static class MSSTweenPositionEditor
    {
        #region GUI

        public static void OnGUI(MSSTweenPosition tween)
        {
            MSSTweenEditor.DrawHeader(tween,
                () => tween.tweenValue = MSSItemEditor.sharedItem.gameObject.transform.localPosition); 

            MSSEditorUtils.DrawGenericProperty(ref tween.tweenValue, tween);
        }

        #endregion
    }
}
