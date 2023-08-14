namespace Ball
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BallCollisionsHandler))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Ball : GameClass
    {
        [SerializeField] private float force;

        private BallColorManager _ballColorManager;
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

        protected override void AtInit()
        {
            _ballColorManager = new BallColorManager();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb2D = GetComponent<Rigidbody2D>();
            GetComponent<BallCollisionsHandler>().Init(GameManager);

            GameManager.MouseBallInput.OnClick.AddListener(Jump);
            PlayerColor = BallColor.Yellow;
            GameManager.StateManager.onStateChanged.AddListener(OnStateChanged);
        }

        private void OnStateChanged(StateEnum state)
        {
            if (state.HasFlag(StateEnum.EndOfGame))
            {
                _rb2D.gravityScale = 0f;
                _rb2D.velocity = Vector2.zero;
            }
            else if (state.HasFlag(StateEnum.Game))
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rb2D.velocity = Vector2.up * force;
        }
    }
}