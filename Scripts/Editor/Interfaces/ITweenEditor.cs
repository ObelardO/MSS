using JetBrains.Annotations;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public interface ITweenEditor
    {
        string Name { get; }

        void OnGUI();

        void OnAddButton();
    }
}
