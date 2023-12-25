using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector2 direction;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float lifeTime;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        Invoke("SelfDestruction", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

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
}
