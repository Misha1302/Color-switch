namespace Data
{
    using GameDataBase.Ui;

    public sealed class GameDataManager : IInitializable
    {
        private GameManager _gameManager;
        private int _scoreCount;
        private string _login;
        private string _password;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Save()
        {
            _scoreCount = _gameManager.ScoreCounter.ScoreCount + _scoreCount;
            _gameManager.FirebaseManager.SetUser(new PlayerDto(_login, _password, _scoreCount));
        }

        public int GetTotalScore() => _scoreCount + GetEarnedScore();

        public int GetEarnedScore() => _gameManager.ScoreCounter.ScoreCount;

        public void SetUser(PlayerDto user)
        {
            _login = user.id;
            _password = user.password;
            _scoreCount = user.score;
        }
    }
}