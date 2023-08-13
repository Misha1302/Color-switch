using UnityEngine;

public class ObstacleRotater : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float maxDelta;

    private float _curSpeed;
    private Vector3 _direction;

    private void Start()
    {
        _curSpeed = defaultSpeed + (Random.value - 0.5f) * maxDelta;
        _direction = Random.value < 0.5f ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);
    }

    private void FixedUpdate()
    {
        transform.Rotate(_direction * _curSpeed);
    }
}