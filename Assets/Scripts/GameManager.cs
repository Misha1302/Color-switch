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
        GameClass input;

        if (Input.touchSupported)
            MouseBallInput = (IBallInput)(input = gameObject.AddComponent<TouchBallInput>());
        else MouseBallInput = (IBallInput)(input = gameObject.AddComponent<MouseBallInput>());

        ScoreCounter = gameObject.AddComponent<ScoreCounter>();

        ScoreCounter.Init(this);
        Ball.Init(this);
        LevelGenerator.Init(this);
        StateManager.Init(this);
        ScoreCounter.Init(this);
        optimizer.Init(this);
        input.Init(this);

        LevelGenerator.InitPredicate(pos => ball.transform.position.y + 40 > pos.y);
    }
}