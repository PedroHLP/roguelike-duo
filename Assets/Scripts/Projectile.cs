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

        Invoke("SelfDestruction", lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }

    private void SelfDestruction(){
        if(this != null){
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
