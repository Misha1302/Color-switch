using UnityEngine;

public sealed class ScoreCounter : MonoBehaviour, IInitializable
{
    private GameManager _gameManager;
    public int ScoreCount => (int)(_gameManager.Ball.transform.position.y / 7);

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}