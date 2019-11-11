using System;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public interface IGenericTweenEditor
    {
        #region Properties

        string Name { get; }
        Type Type { set; get; }
        Action<State> AddAction { set; get; }

        float TotalHeight { get; }
        float HeaderHeight { get; }
        float Height { get; }

        bool IsMultiple { get; }
        bool HasComponent(Tween tween);

        #endregion

        #region Inspector

        void Draw(Rect rect, Tween tween);

        void DrawHeader(Rect rect, Tween tween);

        #endregion
    }
}