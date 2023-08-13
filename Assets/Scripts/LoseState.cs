using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class LoseState : MonoBehaviour, IInitializable
{
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Button restart;
    [SerializeField] private TMP_Text scoreCount;
    private GameManager _gameManager;

    private void Start()
    {
        loseCanvas.enabled = false;

        restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Lose()
    {
        loseCanvas.enabled = true;
        scoreCount.text = string.Format(scoreCount.text, _gameManager.ScoreCounter.ScoreCount);
        // TODO: call saver
    }
}