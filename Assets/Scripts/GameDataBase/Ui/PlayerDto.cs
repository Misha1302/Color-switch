namespace GameDataBase.Ui
{
    using System;

    [Serializable]
    public class PlayerDto : IHaveId
    {
        public string password;
        public int score;
        public string id;

        public PlayerDto(string id, string password, int score)
        {
            this.id = id;
            this.password = password;
            this.score = score;
        }

        public string GetId() => id;
    }
}