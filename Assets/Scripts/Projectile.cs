using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool isCritical;
    private Vector2 direction;

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

        isCritical = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (!isCritical) enemy.DealDamage(damage);
            else enemy.DealDamage(damage * pStatusHandler.statusValues.criticalDamageIncrease);

            OnDamageEnemy?.Invoke(enemy);
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
