using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool isCritical;
    private Vector2 direction;

    public int maxHitsOnEnemies;

    [SerializeField]
    private float lifeTime;

    private PlayerStatusHandler pStatusHandler;

    public Action<EnemyController> OnDamageEnemy;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        pStatusHandler = PlayerStatusHandler.Instance;
        speed += pStatusHandler.statusValues.baseProjectileSpeed;
        rb.velocity = direction.normalized * speed;
        damage += pStatusHandler.statusValues.damage;
        Invoke("SelfDestruction", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            enemy.KnockBack(direction.normalized);

            if (!isCritical) enemy.DealDamage(damage, isCritical);
            else enemy.DealDamage(damage * pStatusHandler.statusValues.criticalDamageIncrease, isCritical);

            OnDamageEnemy?.Invoke(enemy);

            maxHitsOnEnemies--;

            if (maxHitsOnEnemies <= 0 && maxHitsOnEnemies != -100)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void SelfDestruction()
    {
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetDir(Vector2 dir)
    {
        direction = dir;
    }

    public Vector2 GetDir()
    {
        return direction;
    }
}
