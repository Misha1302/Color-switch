using Ball.Input;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private Ball.Ball ball;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private Optimizer optimizer;
    [SerializeField] private StateManager stateManager;
    [SerializeField] private StatisticManager statisticManager;

    public IBallInput MouseBallInput { get; private set; }
    public ScoreCounter ScoreCounter { get; private set; }
    public GameDataManager GameDataManager { get; private set; }

    public Ball.Ball Ball => ball;
    public LevelGenerator LevelGenerator => levelGenerator;
    public StateManager StateManager => stateManager;

    private void Awake()
    {
        var input = InitInput();

        GameDataManager = new GameDataManager();

        ScoreCounter = gameObject.AddComponent<ScoreCounter>();

        GameDataManager.Init(this);
        Ball.Init(this);
        LevelGenerator.Init(this);
        StateManager.Init(this);
        ScoreCounter.Init(this);
        optimizer.Init(this);
        stateManager.Init(this);
        statisticManager.Init(this);
        input.Init(this);

        LevelGenerator.InitPredicate(pos => ball.transform.position.y + 40 > pos.y);
    }

    private void OnApplicationQuit()
    {
        GameDataManager.Save();
    }

    private GameClass InitInput()
    {
        GameClass input;

        if (Input.touchSupported)
            MouseBallInput = (IBallInput)(input = gameObject.AddComponent<TouchBallInput>());
        else MouseBallInput = (IBallInput)(input = gameObject.AddComponent<MouseBallInput>());

        return input;
    }
}