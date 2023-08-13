using UnityEngine.Events;

namespace Ball.Input
{
    public interface IBallInput
    {
        public UnityEvent OnClick { get; }
    }
}