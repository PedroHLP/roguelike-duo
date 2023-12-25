using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int initialPoolSize = 10;
    private List<GameObject> enemyPool;

    [SerializeField]
    private bool spawnOnSides;

    private void Start()
    {
        InitializePool();
        SpawnEnemy();
    }

    private void InitializePool()
    {
        enemyPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetMyEnemySpawner(this);
        enemy.SetActive(false);
        enemyPool.Add(enemy);
    }

    public void SpawnEnemy()
    {
        GameObject enemy = GetInactiveEnemy();

        if (enemy == null)
        {
            CreateEnemy();
            enemy = GetInactiveEnemy();
        }

        enemy.transform.position = GetRandomSpawnPosition();
        enemy.SetActive(true);
    }

    private GameObject GetInactiveEnemy()
    {
        return enemyPool.Find(enemy => !enemy.activeSelf);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x, y;

        if (spawnOnSides)
        {
            x = Random.Range(0f, 1f) > 0.5f ? -11f : 11f;
            y = Random.Range(-7f, 7f);
        }
        else
        {
            x = Random.Range(-11f, 11f);
            y = Random.Range(0f, 1f) > 0.5f ? 7f : -7f;
        }

        return new Vector3(x, y, 0f);
    }

    public void KillEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}