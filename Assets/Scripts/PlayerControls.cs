using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float speed;

    public GameObject bullet;
    public float damage;
    public float bulletSpeed;
    public float bulletLifespan;

    private Vector2 movementVector;
    private Rigidbody2D rb;
    private Camera cam;

    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }

    private void OnAttack()
    {
        GameObject newBullet = Instantiate(bullet);
        // pass damage to the bullet
        newBullet.GetComponent<Bullet>().Initialize(damage);

        // spawn it in front of the gun
        newBullet.transform.position = transform.position + direction.normalized * 0.9f;

        // assign velocity based on player's direction (mousePos)
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        newBulletRb.linearVelocity = direction.normalized * bulletSpeed;

        // destroy after
        Destroy(newBullet, bulletLifespan);
    }

    // FOR TESTING
    private void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementVector * speed;
    }

    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -cam.transform.position.z));

        direction = worldPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
