using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    private float startingLife;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private EnemySpawner myEnemySpawner;

    [SerializeField]
    private GameObject enemyProjectile;
    [SerializeField]
    private float attackSpeed;

    private bool canShoot;

    public float speedDebuff, speedBuff;
    public bool canWalk;

    private void Start()
    {
        playerTransform = PlayerStatusHandler.Instance.gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        canWalk = true;

        speedBuff = 1;
        speedDebuff = 1;

        if (startingLife == 0)
            startingLife = life;
    }

    private void Update()
    {
        if (!canWalk)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (!followAndShoot)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * (speed * speedBuff / speedDebuff);
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= shootingDistance)
            {
                rb.velocity = new Vector2(0, 0);
                Shoot();
            }
            else
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * (speed * speedBuff / speedDebuff);
            }
        }
    }

    private void OnEnable()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.DisableKeyword("HITEFFECT_ON");
        speedDebuff = 1;
        
        if (startingLife != 0)
            life = startingLife;
    }

    private void Shoot()
    {
        if (!canShoot) return;

        Vector2 direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        EnemyProjectile projectile = Instantiate(enemyProjectile, transform.position, quaternion.identity).GetComponent<EnemyProjectile>();
        projectile.SetDir(direction);

        StartCoroutine(ShootCoroutine());
    }

    public void DealDamage(float damage, bool isCritical)
    {
        life -= damage;

        StopCoroutine(Hitted(isCritical));
        StartCoroutine(Hitted(isCritical));

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

    private IEnumerator Hitted(bool isCritical)
    {
        Material mat = GetComponent<Renderer>().material;
        mat.EnableKeyword("HITEFFECT_ON");
        Color originalColor = mat.GetColor("_HitEffectColor");
        if (isCritical)
            mat.SetColor("_HitEffectColor", new Color(1f, 0f, 0f, 1f));
        float startingSpeedDebuf = speedDebuff;
        speedDebuff += 3f;
        yield return new WaitForSeconds(0.15f);
        mat.SetColor("_HitEffectColor", originalColor);
        speedDebuff = startingSpeedDebuf;
        mat.DisableKeyword("HITEFFECT_ON");
    }
}
