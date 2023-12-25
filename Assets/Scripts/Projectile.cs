using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
