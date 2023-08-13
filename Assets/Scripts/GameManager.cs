using Ball.Input;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private Ball.Ball ball;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private Optimizer optimizer;
    [SerializeField] private StateManager stateManager;

    public IBallInput MouseBallInput { get; private set; }
    public ScoreCounter ScoreCounter { get; private set; }

    public Ball.Ball Ball => ball;
    public LevelGenerator LevelGenerator => levelGenerator;
    public StateManager StateManager => stateManager;

    private void Awake()
    {
        if (Input.touchSupported)
            MouseBallInput = gameObject.AddComponent<TouchBallInput>();
        else MouseBallInput = gameObject.AddComponent<MouseBallInput>();

        ScoreCounter = gameObject.AddComponent<ScoreCounter>();

        Ball.Init(this);
        LevelGenerator.Init(this);
        StateManager.Init(this);
        ScoreCounter.Init(this);
        optimizer.Init(this);

        LevelGenerator.InitPredicate(pos => ball.transform.position.y + 40 > pos.y);
    }
}