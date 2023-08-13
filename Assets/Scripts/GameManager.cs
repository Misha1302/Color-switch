using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private Optimizer optimizer;

    public IBallInput MouseBallInput { get; private set; }
    public StateManager StateManager { get; private set; }
    public Ball Ball => ball;
    public LevelGenerator LevelGenerator => levelGenerator;

    private void Awake()
    {
        if (Input.touchSupported)
            MouseBallInput = gameObject.AddComponent<TouchBallInput>();
        else MouseBallInput = gameObject.AddComponent<MouseBallInput>();

        StateManager = new StateManager();

        Ball.Init(this);
        LevelGenerator.Init(this);
        LevelGenerator.InitPredicate(pos => ball.transform.position.y + 40 > pos.y);
        optimizer.Init(this);
    }
}