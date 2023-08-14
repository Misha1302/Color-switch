namespace Data
{
    public sealed class GameDataManager : IInitializable
    {
        private GameManager _gameManager;
        private int _scoreCount;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            Load();
        }

        private void Load()
        {
            var data = DataSaver.Load<GameData>() ?? new GameData(0);
            _scoreCount = data.scoreCount;
        }

        public void Save()
        {
            DataSaver.Save(new GameData(_gameManager.ScoreCounter.ScoreCount + _scoreCount));
        }

        public int GetTotalScore() => _scoreCount + GetEarnedScore();

        public int GetEarnedScore() => _gameManager.ScoreCounter.ScoreCount;
    }
}