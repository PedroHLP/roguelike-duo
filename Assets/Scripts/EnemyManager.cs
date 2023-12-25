using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public int poolSize = 10; // Tamanho do pool
    public float spawnRate = 2f; // Taxa de spawn
    public Transform player; // Referência ao jogador

    private List<GameObject> enemyPool;
    private float nextSpawnTime;

    void Start()
    {
        // Inicializar o pool de inimigos
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        // Verificar se é hora de spawnar um novo inimigo
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                // Defina aqui a lógica para a posição de spawn
                enemy.transform.position = GetSpawnPosition();
                enemy.SetActive(true);
                break;
            }
        }
    }

    Vector3 GetSpawnPosition()
    {
        float spawnRadius = 5f; 
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
        return new Vector3(spawnPos.x, spawnPos.y, 0) + transform.position;
    }
}
