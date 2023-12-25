using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0;
    private Vector2 direction;

    [SerializeField]
    private float lifeTime;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        speed += PlayerStatusHandler.Instance.statusValues.baseProjectileSpeed;
        rb.velocity = direction.normalized * speed;

        StartCoroutine(SelfDestruction());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }

    private IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this);
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
