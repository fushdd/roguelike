using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            // ricochet off walls
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
        }
    }
}
