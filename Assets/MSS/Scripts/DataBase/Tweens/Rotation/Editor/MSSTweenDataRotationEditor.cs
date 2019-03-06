using UnityEngine;
using UnityEditor;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public class MSSTweenDataRotationEditor
    {
        public static void OnGUI(MSSStateData stateData, MSSTweenDataRotation tweenData)
        {
            MSSEditorUtils.DrawGenericProperty(ref tweenData.tweenValue);
        }
    }
}
