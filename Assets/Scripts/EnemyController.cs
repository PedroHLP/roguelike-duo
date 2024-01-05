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

    private bool canShoot, isKnockingBack;

    private Vector2 knockBackDirection;

    public float speedDebuff, speedBuff;
    public bool canWalk;

    [SerializeField]
    private GameObject xpFragment;

    [SerializeField]
    private int xpDropMin, xpDropMax;

    private Animator animator;

    private void Start()
    {
        playerTransform = PlayerStatusHandler.Instance.gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        canWalk = true;
        isKnockingBack = false;

        speedBuff = 1;
        speedDebuff = 1;

        if (startingLife == 0)
            startingLife = life;
    }

    private void Update()
    {
        if (isKnockingBack)
        {
            rb.velocity = knockBackDirection * (speed * 2.8f);
        }
        else
            BasicMovement();
    }

    private void BasicMovement()
    {
        if (!canWalk)
        {
            rb.velocity = Vector2.zero;
            animator.SetInteger("EnemyAnimState", 0);
            return;
        }

        if (!followAndShoot)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * (speed * speedBuff / speedDebuff);
            animator.SetInteger("EnemyAnimState", 1);
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= shootingDistance)
            {
                animator.SetInteger("EnemyAnimState", 0);
                rb.velocity = new Vector2(0, 0);
                Shoot();
            }
            else
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * (speed * speedBuff / speedDebuff);
                animator.SetInteger("EnemyAnimState", 1);
            }
        }
    }

    private void OnEnable()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.DisableKeyword("HITEFFECT_ON");
        speedDebuff = 1;

        animator = GetComponent<Animator>();
        animator.SetInteger("EnemyAnimState", 1);
        animator.SetFloat("WalkingAnimSpeed", speed / 1.6f);

        canShoot = true;
        isKnockingBack = false;

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

    public void KnockBack(Vector2 direction)
    {
        if (life <= 0) return;

        knockBackDirection = direction;
        StartCoroutine(KnockBackCoroutine());
    }

    public void SetMyEnemySpawner(EnemySpawner enemySpawner)
    {
        myEnemySpawner = enemySpawner;
    }

    private void Die()
    {
        int xpDropCount = UnityEngine.Random.Range(xpDropMin, xpDropMax + 1);

        for (int i = 0; i < xpDropCount; i++)
        {
            Instantiate(xpFragment, new Vector2(transform.position.x + UnityEngine.Random.Range(-0.4f, 0.4f), transform.position.y + UnityEngine.Random.Range(-0.4f, 0.4f)), quaternion.identity);
        }

        StopCoroutine(KnockBackCoroutine());
        StopCoroutine(ShootCoroutine());

        myEnemySpawner.KillEnemy(this.gameObject);
    }

    private IEnumerator KnockBackCoroutine()
    {
        isKnockingBack = true;
        yield return new WaitForSeconds(0.15f);
        isKnockingBack = false;
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
        animator.SetInteger("EnemyAnimState", 2);
        yield return new WaitForSeconds(0.15f);
        mat.SetColor("_HitEffectColor", originalColor);
        speedDebuff = startingSpeedDebuf;
        mat.DisableKeyword("HITEFFECT_ON");
    }
}
