using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public class MSSTweenDataPositionEditor
    {
        public static void OnGUI(MSSStateData stateData, MSSTweenDataPosition tweenData)
        {
            MSSEditorUtils.DrawGenericProperty(ref tweenData.tweenValue);
        }
    }
}
