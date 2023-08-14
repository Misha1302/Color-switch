using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace State
{
    public sealed class LoseState : GameClass
    {
        [SerializeField] private Canvas loseCanvas;
        [SerializeField] private Button restart;
        [SerializeField] private TMP_Text earnScoreCount;
        [SerializeField] private TMP_Text totalScoreCount;

        protected override void AtStart()
        {
            loseCanvas.gameObject.SetActive(false);

            restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }

        public void Lose()
        {
            loseCanvas.gameObject.SetActive(true);
            earnScoreCount.text = string.Format(earnScoreCount.text, GameManager.GameDataManager.GetEarnedScore());
            totalScoreCount.text = string.Format(totalScoreCount.text, GameManager.GameDataManager.GetTotalScore());
            GameManager.GameDataManager.Save();
        }
    }
}