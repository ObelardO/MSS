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
            MSSTweenEditor.DrawHeader(tween);
        }

        #endregion
    }
}
