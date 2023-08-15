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
            canvas.gameObject.SetActive(true);

            loginButton.onClick.AddListener(() =>
            {
                GameManager.FirebaseManager.GetUser<PlayerDto>(login.text, user =>
                {
                    if (user == null)
                    {
                        message.text = "User with the same name was not created";
                    }
                    else
                    {
                        if (password.text != user.password)
                        {
                            message.text = "This user have other password";
                        }
                        else
                        {
                            GameManager.GameDataManager.SetUser(user);
                            canvas.gameObject.SetActive(false);
                            GameManager.StateManager.SwitchBeforeStart();
                        }
                    }
                });
            });

            registerButton.onClick.AddListener(() =>
            {
                GameManager.FirebaseManager.GetUser<PlayerDto>(login.text, user =>
                {
                    if (user == null)
                    {
                        GameManager.FirebaseManager.SetUser(new PlayerDto(login.text, password.text, 0));
                    }
                    else
                    {
                        message.text = "User with the same name already added";
                        canvas.gameObject.SetActive(false);
                        GameManager.StateManager.SwitchBeforeStart();
                    }
                });
            });
        }
    }
}