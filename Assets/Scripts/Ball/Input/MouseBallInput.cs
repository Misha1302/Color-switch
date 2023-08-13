using UnityEngine.Events;

namespace Ball.Input
{
    using Input = UnityEngine.Input;

    public sealed class MouseBallInput : GameClass, IBallInput
    {
        protected override void AtUpdate()
        {
            if (Input.GetMouseButtonDown(0))
                OnClick.Invoke();
        }

        public UnityEvent OnClick { get; } = new();
    }
}