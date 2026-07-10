using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float power;

    private PlayerHealth player;
    private Rigidbody2D rb;
    private Vector2 movementVector;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    public float GetPower()
    {
        return power;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out var player))
        {
            player.ReceiveDamage();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = player.transform.position - transform.position;

        rb.linearVelocity = movementVector.normalized * speed;
    }
}
