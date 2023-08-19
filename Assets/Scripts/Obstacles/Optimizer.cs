using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    public sealed class Optimizer : MonoBehaviour, IInitializable
    {
        private readonly SortedList<float, Transform> _listOfObjects = new();

        public void Init(GameManager gameManager)
        {
            gameManager.LevelGenerator.OnGenerateBlock.AddListener(OnGenerateBlock);
            StartCoroutine(Optimize());
        }

        private void OnGenerateBlock(GameObject block)
        {
            var obj = block.transform;
            _listOfObjects.Add(obj.position.sqrMagnitude, obj);

            var main = Camera.main;
            if (main == null)
                return;

            block.SetActive(main.transform.position.y + 20 >= obj.position.y);
        }

        private IEnumerator Optimize()
        {
            while (true)
            {
                var main = Camera.main;
                if (main == null)
                    continue;

                foreach (var (_, obj) in _listOfObjects)
                {
                    if (main.transform.position.y + 20 < obj.position.y)
                        break;

                    obj.gameObject.SetActive(main.transform.position.y < obj.position.y + 20);
                }

                yield return new WaitForSeconds(10f / 3f);
            }

            // ReSharper disable once IteratorNeverReturns
        }
    }
}