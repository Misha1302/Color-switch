namespace State
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class StartState : MonoBehaviour, IInitializable
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button start;
        private GameManager _gameManager;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;

            _gameManager.StateManager.onStateChanged.AddListener(state =>
            {
                if (state.HasFlag(StateEnum.BeforeStart))
                    canvas.gameObject.SetActive(true);
            });

            start.onClick.AddListener(() =>
            {
                if (!_gameManager.StateManager.StateEnum.HasFlag(StateEnum.BeforeStart))
                    return;

                canvas.gameObject.SetActive(false);
                _gameManager.StateManager.SwitchGame();
            });
        }
    }
}