using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public sealed class BallCollisionsHandler : MonoBehaviour, IInitializable
    {
        private GameManager _gameManager;
        private List<int> _mask;

        private void Start()
        {
            _mask = new List<int>
            {
                LayerMask.NameToLayer("Yellow"),
                LayerMask.NameToLayer("Blue"),
                LayerMask.NameToLayer("Pink"),
                LayerMask.NameToLayer("Violet")
            };
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_mask.Contains(other.gameObject.layer))
                return;

            var layerName = LayerMask.LayerToName(other.gameObject.layer);
            var colorName = _gameManager.Ball.PlayerColor.ToString();
            if (layerName != colorName)
                _gameManager.StateManager.Lose();
        }

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
    }
}