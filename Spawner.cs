using System.Collections;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _spawnField;

    private Transform[] _spawnPoints;
    private int _enemyCount = 10;
    private float _delay = 2;

    private void Start()
    {
        _spawnPoints = GetSpawnPoints(_spawnField);
        StartCoroutine(SpawnWithDelay(_delay, _enemy.gameObject, _enemyCount, _spawnPoints));
    }

    private Transform[] GetSpawnPoints(GameObject spawnField)
    {
        Transform[] _spawnPoints = spawnField.GetComponentsInChildren<Transform>().
               OrderBy(transform => transform.gameObject.name).ToArray();
        return _spawnPoints.Except(new Transform[] { spawnField.GetComponent<Transform>() }).ToArray();
    }

    private IEnumerator SpawnWithDelay(float delay, GameObject gameObject, int count, Transform[] positions)
    {
        var waitForSeconds = new WaitForSeconds(delay);
        int index = 0;

        for (int i = 0; i < count; i++)
        {
            if (index >= positions.Length)
            {
                index -= positions.Length;
            }

            Instantiate(gameObject, positions[index].position, Quaternion.identity);
            index++;
            yield return waitForSeconds;
        }
    }
}
