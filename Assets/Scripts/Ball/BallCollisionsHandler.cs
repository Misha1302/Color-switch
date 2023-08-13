using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public sealed class BallCollisionsHandler : GameClass
    {
        private List<int> _mask;

        protected override void AtStart()
        {
            _mask = new List<int>
            {
                LayerMask.NameToLayer("Yellow"),
                LayerMask.NameToLayer("Blue"),
                LayerMask.NameToLayer("Pink"),
                LayerMask.NameToLayer("Violet")
            };
        }

        protected override void AtOnTriggerEnter2D(Collider2D other)
        {
            if (!_mask.Contains(other.gameObject.layer))
                return;

            var layerName = LayerMask.LayerToName(other.gameObject.layer);
            var colorName = GameManager.Ball.PlayerColor.ToString();
            if (layerName != colorName)
                GameManager.StateManager.Lose();
        }
    }
}