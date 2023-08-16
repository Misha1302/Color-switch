namespace GameDataBase.Ui
{
    using System;

    [Serializable]
    public class PlayerDto : IHaveId
    {
        public string password;
        public int score;
        public string login;

        public PlayerDto(string login, string password, int score)
        {
            this.login = login;
            this.password = password;
            this.score = score;
        }

        public string GetId() => login;
    }
}