namespace Data
{
    using UnityEngine;

    public sealed class HeightCounter : GameClass
    {
        private readonly ScoreCounter _scoreCounter = new();
        public int ScoreCount => _scoreCounter.GetScoreCount(GameManager.Ball.transform.position.y);
        public float MaxY { get; private set; }

        protected override void AtFixedUpdate()
        {
            MaxY = Mathf.Max(MaxY, GameManager.Ball.transform.position.y);
        }
    }
}