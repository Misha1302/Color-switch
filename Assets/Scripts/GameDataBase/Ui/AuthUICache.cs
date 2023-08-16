namespace GameDataBase.Ui
{
    public sealed class AuthUICache
    {
        public readonly string Login;
        public readonly string Password;

        public AuthUICache(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}