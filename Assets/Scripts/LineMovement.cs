using UnityEngine;

public sealed class LineMovement : MonoBehaviour
{
    [Header("Hangs on an object upon contact with which the panel teleports to startPos")]
    [SerializeField] private Transform startPos;

    [SerializeField] private Transform line;
    [SerializeField] private float lineDirection;
    [SerializeField] private float maxDelta;

    private Vector3 _direction;

    private void Start()
    {
        _direction = new Vector3(lineDirection + Random.Range(-maxDelta, maxDelta), 0, 0);
    }

    private void FixedUpdate()
    {
        line.position += _direction;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.transform.position = startPos.position;
    }
}