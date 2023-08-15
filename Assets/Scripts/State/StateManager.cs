using UnityEngine;
using UnityEngine.Events;

namespace State
{
    public sealed class StateManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private LoseState loseState;
        [SerializeField] private StartState startState;

        [HideInInspector] public UnityEvent<StateEnum> onStateChanged = new();

        public StateEnum StateEnum { get; private set; } = StateEnum.Auth;

        public void Init(GameManager gameManager)
        {
            loseState.Init(gameManager);
            startState.Init(gameManager);
            onStateChanged.Invoke(StateEnum);
        }

        public void SwitchLose()
        {
            StateEnum = StateEnum.Lose;
            loseState.Lose();
            onStateChanged.Invoke(StateEnum);
        }

        public void SwitchGame()
        {
            StateEnum = StateEnum.Game;
            onStateChanged.Invoke(StateEnum);
        }

        public void SwitchStart()
        {
            StateEnum = StateEnum.Start;
            onStateChanged.Invoke(StateEnum);
        }

        public void SwitchBeforeStart()
        {
            StateEnum = StateEnum.BeforeStart;
            onStateChanged.Invoke(StateEnum);
        }
    }
}