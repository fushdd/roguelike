using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    private SpriteRenderer healthState1;
    private SpriteRenderer healthState2;
    private SpriteRenderer healthState3;

    [SerializeField] private GameObject healthIndicator;
    [SerializeField] private GameObject player;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        healthState1 = healthIndicator.transform.Find("HealthState1").GetComponent<SpriteRenderer>();
        healthState2 = healthIndicator.transform.Find("HealthState2").GetComponent<SpriteRenderer>();
        healthState3 = healthIndicator.transform.Find("HealthState3").GetComponent<SpriteRenderer>();

        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        float health = playerHealth.GetHealth();

        if (health % 1 != 0) return;

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        switch (health)
        {
            case (3):
                healthState1.enabled = true;
                // disable all following health states
                healthState2.enabled = false;
                healthState3.enabled = false;
                break;

            case (2):
                healthState2.enabled = true;
                // disable all following health states
                healthState3.enabled = false;
                break;

            case (1):
                healthState3.enabled = true;
                break;

            default:
                // disable all following health states
                healthState1.enabled = false;
                healthState2.enabled = false;
                healthState3.enabled = false;
                break;
        }
    }
}
