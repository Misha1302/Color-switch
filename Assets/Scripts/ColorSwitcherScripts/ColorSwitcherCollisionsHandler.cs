namespace ColorSwitcherScripts
{
    using System;
    using Ball;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [RequireComponent(typeof(Animator))]
    public sealed class ColorSwitcherCollisionsHandler : MonoBehaviour, IInitializable
    {
        private readonly Array _enumValues = typeof(BallColor).GetEnumValues();
        private Animator _animator;
        private GameManager _gameManager;
        private bool _stop;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_stop) return;
            var ballColor = (BallColor)_enumValues.GetValue(Random.Range(0, _enumValues.Length));
            while (ballColor == _gameManager.Ball.PlayerColor)
                ballColor = (BallColor)_enumValues.GetValue(Random.Range(0, _enumValues.Length));
            
            _gameManager.Ball.PlayerColor = ballColor;
            _animator.enabled = false;
            _stop = true;
        }

        public void Init(GameManager gameManager)
        {
            _animator = GetComponent<Animator>();
            _gameManager = gameManager;
        }
    }
}