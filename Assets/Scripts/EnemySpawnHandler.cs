using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    public List<EnemySpawnerSettings> enemySpawners;

    private float elapsedTime;

    private void Update()
    {
        elapsedTime = Time.time;


        foreach (EnemySpawnerSettings enemySpawner in enemySpawners)
        {
            if (elapsedTime >= enemySpawner.timeToStartToSpawn) enemySpawner.HandleSpawns(elapsedTime);
        }
    }

    private void OnEnable()
    {
        foreach (EnemySpawnerSettings enemySpawner in enemySpawners)
        {
            enemySpawner.enemySpawner.OnEnemySpawn += enemySpawner.IncreaseCount;
            enemySpawner.enemySpawner.OnEnemyDie += enemySpawner.DecreaseCount;
        }
    }

    private void OnDisable()
    {
        foreach (EnemySpawnerSettings enemySpawner in enemySpawners)
        {
            enemySpawner.enemySpawner.OnEnemySpawn -= enemySpawner.IncreaseCount;
            enemySpawner.enemySpawner.OnEnemyDie -= enemySpawner.DecreaseCount;
        }
    }

}

[Serializable]
public class EnemySpawnerSettings
{
    public EnemySpawner enemySpawner;
    public float timeToStartToSpawn;
    public int currentCount;

    public int limitEnemyAliveAtTheMoment;
    public int maxLimitEnemyAliveAtThemoment;

    public float increaseLimitEnemyAliveRate;
    public int increaseLimitEnemyAliveQuantity;

    private float startingTime;

    public void IncreaseCount()
    {
        currentCount++;
    }

    public void DecreaseCount()
    {
        currentCount--;
    }

    public void HandleSpawns(float timeElapsed)
    {
        if (currentCount < limitEnemyAliveAtTheMoment) enemySpawner.SpawnEnemy();

        float timeElapsedSinceTheLastStart = timeElapsed - startingTime;

        if (timeElapsedSinceTheLastStart >= increaseLimitEnemyAliveRate)
        {
            limitEnemyAliveAtTheMoment += increaseLimitEnemyAliveQuantity;
            startingTime = timeElapsed;
        }

        if (limitEnemyAliveAtTheMoment > maxLimitEnemyAliveAtThemoment) limitEnemyAliveAtTheMoment = maxLimitEnemyAliveAtThemoment;
    }

}
