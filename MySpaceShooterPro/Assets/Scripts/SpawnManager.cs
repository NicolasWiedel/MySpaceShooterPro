using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _powerupContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!_stopSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!_stopSpawning)
        {
            SpawnPoweup();
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(_enemyPrefab,
            new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0.0f),
            Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    private void SpawnPoweup()
    {
        int randowPowerup = Random.Range(0, 3);
        GameObject newPowerup = Instantiate(powerups[randowPowerup],
            new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0.0f),
            Quaternion.identity);
        newPowerup.transform.parent = _powerupContainer.transform;
    }

    public void OnPlayerDead()
    {
        _stopSpawning = true;
    }
}
