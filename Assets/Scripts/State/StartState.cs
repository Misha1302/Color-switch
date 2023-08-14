using UnityEngine;
using UnityEngine.UI;

public sealed class StartState : MonoBehaviour, IInitializable
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button start;
    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        canvas.gameObject.SetActive(true);
        _gameManager = gameManager;
        
        start.onClick.AddListener(() =>
        {
            canvas.gameObject.SetActive(false);
            _gameManager.StateManager.StartGame();
        });
    }
}