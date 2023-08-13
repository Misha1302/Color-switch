using System;
using UnityEngine;

namespace Ball
{
    public class BallColorManager
    {
        public Color BallColorToColor(BallColor color)
        {
            return color switch
            {
                BallColor.Blue => Color.cyan,
                BallColor.Pink => new Color(1f, 0f, 0.5058824f),
                BallColor.Yellow => Color.yellow,
                BallColor.Violet => new Color(0.63f, 0.25f, 1f),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}