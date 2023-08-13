using UnityEngine;

public class ObstacleRotater : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float maxDelta;
    [SerializeField] private Vector3 direction;

    private float _curSpeed;
    private Vector3 _direction;

    private void Start()
    {
        _curSpeed = defaultSpeed + (Random.value - 0.5f) * maxDelta;
        _direction =
            direction == default
                ? Random.value < 0.5f ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1)
                : direction;
    }

    private void FixedUpdate()
    {
        transform.Rotate(_direction * _curSpeed);
    }
}