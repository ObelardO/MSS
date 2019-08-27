﻿using System;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public interface IGenericTweenEditor
    {
        #region Properties

        string Name { get; }

        Type Type { set; get; }

        Action AddAction { set; get; }

        float TotalHeight { get; }
        float HeaderHeight { get; }
        float Height { set; get; }

        #endregion

        #region Inspector

        //void DrawValueField(Rect rect, Tween tween);

        void Draw(Rect rect, Tween tween);

        void DrawHeader(Rect rect, Tween tween);

        #endregion
    }
}