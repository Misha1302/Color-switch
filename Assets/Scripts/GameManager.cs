using System.Linq;
using Ball.Input;
using Data;
using Firebase;
using GameDataBase;
using GameDataBase.Ui;
using Obstacles;
using State;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private Ball.Ball ball;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private StateManager stateManager;
    [SerializeField] private StatisticManager statisticManager;
    [SerializeField] private PlayersList playersList;
    [SerializeField] private MaxYPanels maxYPanel;

    public IBallInput MouseBallInput { get; private set; }
    public HeightCounter HeightCounter { get; private set; }
    public GameDataManager GameDataManager { get; private set; }
    public FirebaseManager FirebaseManager { get; private set; }

    public Ball.Ball Ball => ball;
    public LevelGenerator LevelGenerator => levelGenerator;
    public StateManager StateManager => stateManager;

    private void Awake()
    {
        var input = InitInput();
        var optimizer = gameObject.AddComponent<Optimizer>();
        HeightCounter = gameObject.AddComponent<HeightCounter>();
        FirebaseManager = gameObject.AddComponent<FirebaseManager>();

        GameDataManager = new GameDataManager();

        GameDataManager.Init(this);
        Ball.Init(this);
        LevelGenerator.Init(this);
        StateManager.Init(this);
        HeightCounter.Init(this);
        stateManager.Init(this);
        statisticManager.Init(this);
        playersList.Init(this);
        maxYPanel.Init(this);
        input.Init(this);
        optimizer.Init(this);

        FindObjectsOfType<AuthUi>().ToList().ForEach(x => x.Init(this));

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