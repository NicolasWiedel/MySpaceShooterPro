using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        yield return null; //wait for one frame
        SpawnEnemy();

        while (_stopSpawning)
        {
            yield return new WaitForSeconds(5.0f);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(_enemyPrefab,
            new Vector3(Random.Range(-11.0f, 11.0f), 8.0f, 0.0f),
            Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    public void OnPlayerDead()
    {
        _stopSpawning = true;
    }
}
