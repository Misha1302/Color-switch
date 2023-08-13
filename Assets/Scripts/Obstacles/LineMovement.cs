namespace Obstacles
{
    using UnityEngine;

    public sealed class LineMovement : GameClass
    {
        [Header("Hangs on an object upon contact with which the panel teleports to startPos")]
        [SerializeField] private Transform startPos;

        [SerializeField] private Transform line;
        [SerializeField] private float lineDirection;
        [SerializeField] private float maxDelta;

        private Vector3 _direction;

        protected override void AtStart()
        {
            _direction = new Vector3(lineDirection + Random.Range(-maxDelta, maxDelta), 0, 0);
        }

        protected override void AtFixedUpdate()
        {
            line.position += _direction;
        }

        protected override void AtOnTriggerEnter2D(Collider2D other)
        {
            other.transform.position = startPos.position;
        }
    }
}