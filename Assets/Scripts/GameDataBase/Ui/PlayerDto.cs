namespace GameDataBase.Ui
{
    using System;

    [Serializable]
    public class PlayerDto : IHaveId
    {
        public string password;
        public int score;
        public string login;
        public float maxY;

        public PlayerDto(string login, string password, int score, float maxY)
        {
            this.login = login;
            this.password = password;
            this.score = score;
            this.maxY = maxY;
        }

        public string GetId() => login;
    }
}