using UnityEngine;

public sealed class StateManager : MonoBehaviour, IInitializable
{
    [SerializeField] private LoseState loseState;
    [SerializeField] private StartState startState;

    public StateEnum StateEnum { get; private set; } = StateEnum.Start;

    public void Init(GameManager gameManager)
    {
        loseState.Init(gameManager);
        startState.Init(gameManager);
    }

    public void Lose()
    {
        loseState.Lose();
    }

    public void StartGame()
    {
        StateEnum = StateEnum.Game;
    }
}