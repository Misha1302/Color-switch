using UnityEngine;
using UnityEngine.Events;

public sealed class MouseBallInput : MonoBehaviour, IBallInput
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick.Invoke();
    }

    public UnityEvent OnClick { get; } = new();
}