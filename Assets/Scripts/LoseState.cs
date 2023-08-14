using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class LoseState : GameClass
{
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Button restart;
    [SerializeField] private TMP_Text scoreCount;

    protected override void AtStart()
    {
        loseCanvas.gameObject.SetActive(false);

        restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void Lose()
    {
        loseCanvas.gameObject.SetActive(true);
        scoreCount.text = string.Format(scoreCount.text, GameManager.ScoreCounter.ScoreCount);
        GameManager.GameDataManager.Save();
    }
}