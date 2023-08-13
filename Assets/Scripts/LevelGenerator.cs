using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            var list = MGetComponentInChildren<IInitializable>(obj);
            if (list.Any())
                list.ForEach(x => x.Init(_gameManager));

            OnGenerateBlock.Invoke(obj.gameObject);

            var posCopy = position;
            // ReSharper disable once AccessToModifiedClosure
            yield return new WaitUntil(() => _whenSpawnNextBlock(posCopy));
        }

        // ReSharper disable once IteratorNeverReturns
    }

    private static List<T> MGetComponentInChildren<T>(Transform t, List<T> comps = null)
    {
        comps ??= new List<T>();

        var collection = t.GetComponents<T>();
        if (collection.Length != 0)
            comps.AddRange(collection);

        for (var i = 0; i < t.childCount; i++)
            MGetComponentInChildren(t.GetChild(i), comps);

        return comps;
    }
}