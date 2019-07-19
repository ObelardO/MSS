using System;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public interface ITweenEditor
    {
        string Name { get; }

        void OnGUI(Rect rect, Tween tween);
    }
}
