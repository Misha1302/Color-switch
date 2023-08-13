using UnityEngine;
using UnityEngine.UI;

public sealed class StartState : MonoBehaviour
{
    [SerializeField] private Button start;
    private GameManager _gameManager;

    private void Start()
    {
        start.onClick.AddListener(() =>
        {
            start.GetComponentInParent<Canvas>().enabled = false;
            _gameManager.StateManager.StartGame();
        });
    }

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}