using UnityEngine;
using UnityEngine.Events;

public sealed class TouchBallInput : MonoBehaviour, IBallInput
{
    private void Update()
    {
        if (Input.touches.Length != 0 && Input.touches[0].phase == TouchPhase.Began)
            OnClick.Invoke();
    }

    public UnityEvent OnClick { get; } = new();
}