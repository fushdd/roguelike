using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] private float timeBeforeRegeneration;
    [SerializeField] private float regenerationPerSecond;

    private bool isInvulnerable = false;
    private float regenerationCooldown;
    private float healthRegenerated; // to regen health every second not frame

    public void ReceiveDamage()
    {
        if (isInvulnerable) return;

        health -= 1;
        regenerationCooldown = timeBeforeRegeneration + invulnerabilityTime; // start the timer when invuln ends
        healthRegenerated = 0;

        StartCoroutine(BecomeInvulnerable());

    }

    public float GetHealth()
    {
        return health;
    }

    IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;

        float timer = invulnerabilityTime;

        // visual representation
        while (timer > 0)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            // (I_I)
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = !transform.GetChild(0).GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.15f);
            timer -= 0.15f;
        }

        GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        isInvulnerable = false;
    }

    private void Update()
    {

        if (health >= 4) return;

        if (regenerationCooldown > 0)
        {
            regenerationCooldown -= Time.deltaTime;
            return;
        }

        // regenerate health every second
        healthRegenerated += regenerationPerSecond * Time.deltaTime;
        if (healthRegenerated >= 1)
        {
            health++;
            healthRegenerated = 0;
        }
    }

}
