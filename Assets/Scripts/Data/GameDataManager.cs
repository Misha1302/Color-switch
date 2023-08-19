namespace Data
{
    using GameDataBase;
    using GameDataBase.Ui;
    using UnityEngine;

    public sealed class GameDataManager : IInitializable
    {
        private GameManager _gameManager;
        private PlayerDto _player;

        public REvent ReadyEvent;

        public float MaxY => _player.maxY;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            ReadyEvent = gameManager.gameObject.AddComponent<REvent>();
        }

        public void Save()
        {
            _player.score += _gameManager.HeightCounter.ScoreCount;
            _player.maxY = Mathf.Max(_player.maxY, _gameManager.HeightCounter.MaxY);
            _gameManager.FirebaseManager.SetUser(_player);
        }

        public int GetTotalScore() => _player.score + GetEarnedScore();

        public int GetEarnedScore() => _gameManager.HeightCounter.ScoreCount;

        public void SetUser(PlayerDto user)
        {
            _player = user;
            ReadyEvent.Invoke();
        }
    }
}