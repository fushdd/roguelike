using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifespan;
    [SerializeField] private float attackCooldown;

    [SerializeField] private AudioClip[] gunshotAudioClips;

    private Vector2 movementVector;
    private Rigidbody2D rb;
    private Camera cam;
    private AudioSource audioSource;

    private Vector3 direction;
    private float curAttackCooldown = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }

    private void OnAttack()
    {
        if (curAttackCooldown > 0) return;

        GameObject newBullet = Instantiate(bullet);
        // pass damage to the bullet
        newBullet.GetComponent<Bullet>().Initialize(damage);

        // spawn it in front of the gun
        newBullet.transform.position = transform.position + direction.normalized * 0.9f;

        // assign velocity based on player's direction (mousePos)
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        newBulletRb.linearVelocity = direction.normalized * bulletSpeed;

        // start the cooldown
        curAttackCooldown = attackCooldown;

        // play the audio
        audioSource.generator = gunshotAudioClips[Random.Range(0, gunshotAudioClips.Length)];
        audioSource.Play();

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

        // process attack cooldown
        if (curAttackCooldown > 0)
        {
            curAttackCooldown -= Time.deltaTime;
        }
    }
}
