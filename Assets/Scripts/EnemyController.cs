using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private float life;
    [SerializeField]
    private float shootingDistance;

    [SerializeField]
    private bool followAndShoot;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private EnemySpawner myEnemySpawner;

    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private float attackSpeed;

    private bool canShoot;

    private void Start()
    {
        playerTransform = PlayerStatusHandler.Instance.gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
    }

    private void Update()
    {
        if (!followAndShoot)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= shootingDistance)
            {
                Shoot();
            }
            else
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        StartCoroutine(ShootCoroutine());
    }

    public void DealDamage(float damage)
    {
        life -= damage;

        if (life < 0) Die();
    }

    public void SetMyEnemySpawner(EnemySpawner enemySpawner)
    {
        myEnemySpawner = enemySpawner;
    }

    private void Die()
    {
        myEnemySpawner.KillEnemy(this.gameObject);
    }

    private IEnumerator ShootCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(1 / attackSpeed);
        canShoot = true;
    }
}
