using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public sealed class LevelGenerator : MonoBehaviour, IInitializable
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float distanceDelta;
    [SerializeField] private Vector3 startPosition;

    public readonly UnityEvent<GameObject> OnGenerateBlock = new();

    private GameManager _gameManager;
    private Func<Vector3, bool> _whenSpawnNextBlock;

    private void Start()
    {
        StartCoroutine(GenerateLevel());
    }

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void InitPredicate(Func<Vector3, bool> whenSpawnNextBlock)
    {
        _whenSpawnNextBlock = whenSpawnNextBlock;
    }

    private IEnumerator GenerateLevel()
    {
        var position = startPosition;
        var oldIndex = -1;
        while (true)
        {
            var rndIndex = Random.Range(0, obstacles.Length);
            while (rndIndex == oldIndex)
                rndIndex = Random.Range(0, obstacles.Length);

            oldIndex = rndIndex;

            var obj = Instantiate(obstacles[oldIndex]).transform;
            obj.position = position;
            position += new Vector3(0, distanceDelta, 0);

            if (TryGetComponentInChildren<IInitializable>(obj, out var initializable))
                initializable.Init(_gameManager);

            OnGenerateBlock.Invoke(obj.gameObject);

            var posCopy = position;
            // ReSharper disable once AccessToModifiedClosure
            yield return new WaitUntil(() => _whenSpawnNextBlock(posCopy));
        }

        // ReSharper disable once IteratorNeverReturns
    }

    private static bool TryGetComponentInChildren<T>(Transform t, out T comp)
    {
        if (t.TryGetComponent(out comp))
            return true;

        for (var i = 0; i < t.childCount; i++)
            if (TryGetComponentInChildren(t.GetChild(i), out comp))
                return true;

        return false;
    }
}