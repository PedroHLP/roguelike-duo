using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int initialPoolSize = 10;
    public float spawnInterval = 2f;
    public float spawnRateIncreaseInterval = 10f;
    public int spawnRateIncreaseAmount = 1;

    private List<GameObject> enemyPool;
    private float nextSpawnTime;
    private float nextSpawnRateIncreaseTime;

    [SerializeField]
    private bool spawnOnSides;

    void Start()
    {
        InitializePool();

        nextSpawnTime = Time.time + spawnInterval;

        nextSpawnRateIncreaseTime = Time.time + spawnRateIncreaseInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (Time.time >= nextSpawnRateIncreaseTime)
        {
            IncreaseSpawnRate();
            nextSpawnRateIncreaseTime = Time.time + spawnRateIncreaseInterval;
        }
    }

    void InitializePool()
    {
        enemyPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetMyEnemySpawner(this);
        enemy.SetActive(false);
        enemyPool.Add(enemy);
    }

    void SpawnEnemy()
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

    GameObject GetInactiveEnemy()
    {
        return enemyPool.Find(enemy => !enemy.activeSelf);
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x, y;

        if (spawnOnSides)
        {
            x = Random.Range(0f, 1f) > 0.5f ? -9f : 9f;
            y = Random.Range(-5f, 5f);
        }
        else
        {
            x = Random.Range(-9f, 9f);
            y = Random.Range(0f, 1f) > 0.5f ? 5f : -5f;
        }

        return new Vector3(x, y, 0f);
    }

    void IncreaseSpawnRate()
    {
        spawnInterval -= spawnRateIncreaseAmount;
        spawnInterval = Mathf.Max(spawnInterval, 0.5f);
    }

    public void KillEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}