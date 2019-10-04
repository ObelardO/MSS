﻿using System;
using UnityEngine;

namespace Obel.MSS.Editor
{
    internal interface IGenericTweenEditor
    {
        #region Properties

        string Name { get; }
        Type Type { set; get; }
        Action AddAction { set; get; }

        float TotalHeight { get; }
        float HeaderHeight { get; }
        float Height { get; }

        bool Multiple { get; }

        #endregion

        #region Inspector

        void Draw(Rect rect, Tween tween);

        void DrawHeader(Rect rect, Tween tween);

        #endregion
    }
}