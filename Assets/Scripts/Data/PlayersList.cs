namespace Data
{
    using System.Collections;
    using System.Linq;
    using System.Text;
    using GameDataBase.Ui;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class PlayersList : GameClass
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button button;

        protected override void AtInit()
        {
            inputField.readOnly = true;

            button.onClick.AddListener(() =>
            {
                SetInputFieldText();
                inputField.gameObject.SetActive(!inputField.gameObject.activeSelf);
            });

            GameManager.FirebaseManager.initEvent.Subscribe(() => GameManager.StartCoroutine(SlowUpdate()));
        }

        private IEnumerator SlowUpdate()
        {
            while (true)
            {
                if (inputField.gameObject.activeSelf)
                    SetInputFieldText();

                yield return new WaitForSecondsRealtime(1f);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void SetInputFieldText()
        {
            GameManager.FirebaseManager.GetAllUsers<PlayerDto>(list =>
            {
                var sortedList = list.OrderBy(x => -x.score).ToList();
                var text = new StringBuilder(sortedList.Count * 10);
                for (var index = 0; index < sortedList.Count; index++)
                {
                    var x = sortedList[index];
                    text.AppendLine($"{index + 1}. {x.login} - {x.score}");
                }

                inputField.text = text.ToString();
            });
        }
    }
}