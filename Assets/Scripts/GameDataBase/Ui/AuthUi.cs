namespace GameDataBase.Ui
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class AuthUi : GameClass
    {
        [SerializeField] private TMP_InputField login;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private TMP_Text message;
        [SerializeField] private Canvas canvas;

        [SerializeField] private Button loginButton;
        [SerializeField] private Button registerButton;

        protected override void AtStart()
        {
            if (StaticData.AuthCache.Login != string.Empty && StaticData.AuthCache.Password != string.Empty)
            {
                Login();
                return;
            }


            canvas.gameObject.SetActive(true);

            loginButton.onClick.AddListener(Login);

            registerButton.onClick.AddListener(Register);
        }

        private void Register()
        {
            var (loginTxt, passwordTxt) = GetLoginAndPassword();

            GameManager.FirebaseManager.GetUser<PlayerDto>(loginTxt, user =>
            {
                if (user == null)
                    StartGame(new PlayerDto(loginTxt, passwordTxt, 0));
                else
                    message.text = "User with the same name already added";
            });
        }

        private void Login()
        {
            var (loginTxt, passwordTxt) = GetLoginAndPassword();

            GameManager.FirebaseManager.GetUser<PlayerDto>(loginTxt, user =>
            {
                if (user == null)
                {
                    message.text = "User with the same name was not created";
                }
                else
                {
                    if (passwordTxt != user.password)
                        message.text = "This user have other password";
                    else
                        StartGame(user);
                }
            });
        }

        private void StartGame(PlayerDto user)
        {
            StaticData.AuthCache = new AuthUICache(user.login, user.password);

            GameManager.GameDataManager.SetUser(user);
            canvas.gameObject.SetActive(false);
            GameManager.StateManager.SwitchBeforeStart();
        }


        private (string login, string password) GetLoginAndPassword()
        {
            var loginTxt = string.IsNullOrEmpty(login.text) ? StaticData.AuthCache.Login : login.text;
            var passwordTxt = string.IsNullOrEmpty(password.text) ? StaticData.AuthCache.Password : password.text;
            return (loginTxt, passwordTxt);
        }
    }
}