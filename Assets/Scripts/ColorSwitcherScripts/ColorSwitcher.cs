namespace ColorSwitcherScripts
{
    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(ColorSwitcherCollisionsHandler))]
    public sealed class ColorSwitcher : MonoBehaviour, IInitializable
    {
        private readonly Color[] _colors =
        {
            Color.yellow,
            Color.blue,
            Color.magenta,
            new(0.63f, 0.25f, 1f),
            Color.cyan,
            Color.red
        };

        private int _dstColorInt;
        private SpriteRenderer _renderer;
        private int _srcColorInt;
        private float _time;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _srcColorInt = 0;
            _dstColorInt = Random.Range(1, _colors.Length);
        }

        private void Update()
        {
            _time += Time.deltaTime;

            var color = Color.Lerp(_colors[_srcColorInt], _colors[_dstColorInt], Mathf.PingPong(_time * 0.4f, 1));
            _renderer.material.color = color;

            if (!EqualsColors(color, _colors[_dstColorInt])) return;

            _time = 0;
            ChangeColor();
        }

        public void Init(GameManager gameManager)
        {
            GetComponent<ColorSwitcherCollisionsHandler>().Init(gameManager);
        }

        private void ChangeColor()
        {
            _srcColorInt = _dstColorInt;

            _dstColorInt = Random.Range(0, _colors.Length);
            while (_dstColorInt == _srcColorInt)
                _dstColorInt = Random.Range(0, _colors.Length);
        }

        private static bool EqualsColors(Color a, Color b) => EqualsFloats(a.a, b.a)
                                                              && EqualsFloats(a.r, b.r)
                                                              && EqualsFloats(a.g, b.g)
                                                              && EqualsFloats(a.b, b.b);

        private static bool EqualsFloats(float a, float b) => Math.Abs(a - b) < 0.01f;
    }
}