using UnityEngine;
using UnityEngine.Events;

namespace Ball.Input
{
    using Input = UnityEngine.Input;

    public sealed class TouchBallInput : GameClass, IBallInput
    {
        protected override void AtUpdate()
        {
            if (Input.touches.Length != 0 && Input.touches[0].phase == TouchPhase.Began)
                OnClick.Invoke();
        }

        public UnityEvent OnClick { get; } = new();
    }
}