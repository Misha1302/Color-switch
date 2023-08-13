using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BallCollisionsHandler))]
[RequireComponent(typeof(SpriteRenderer))]
public sealed class Ball : MonoBehaviour, IInitializable
{
    [SerializeField] private float force;

    private BallColorManager _ballColorManager;
    private GameManager _gameManager;
    private BallColor _playerColor;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _spriteRenderer;

    public BallColor PlayerColor
    {
        get => _playerColor;
        set
        {
            _spriteRenderer.color = _ballColorManager.BallColorToColor(value);
            _playerColor = value;
        }
    }

    public void Init(GameManager gameManager)
    {
        _ballColorManager = new BallColorManager();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = gameManager;
        _rb2D = GetComponent<Rigidbody2D>();
        GetComponent<BallCollisionsHandler>().Init(gameManager);

        _gameManager.MouseBallInput.OnClick.AddListener(Jump);
        PlayerColor = BallColor.Yellow;
    }

    private void Jump()
    {
        _rb2D.velocity = Vector2.up * force;
    }
}