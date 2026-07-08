using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float damage)
    {
        this.damage = damage;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ReceiveDamage(damage);
            Destroy(gameObject);
        }

        // ricochet off walls
        switch (collision.gameObject.name)
        {
            
            case "Left":
                if (rb.linearVelocityX > 0)
                {
                    rb.linearVelocityX = -rb.linearVelocityX;
                }
                break;

            case "Right":
                if (rb.linearVelocityX < 0)
                {
                    rb.linearVelocityX = -rb.linearVelocityX;
                }
                break;

            case "Top":
                if (rb.linearVelocityY < 0)
                {
                    rb.linearVelocityY = -rb.linearVelocityY;
                }
                break;

            case "Bottom":
                if (rb.linearVelocityY > 0)
                {
                    rb.linearVelocityY = -rb.linearVelocityY;
                }
                break;

            // destroy the bullet if it got inside a wall somehow
            case "Inside":
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }
}
