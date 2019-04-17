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
            MSSTweenEditor.DrawHeader(tween);
        }

        #endregion
    }
}