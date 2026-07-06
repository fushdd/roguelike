using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float invulnerabilityTime;
    public float timeBeforeRegeneration;
    public float regenerationPerSecond;

    private bool isInvulnerable = false;
    private float regenerationCooldown;
    private float healthRegenerated; // to regen health every second not frame

    public void Damage()
    {
        if (isInvulnerable) return;

        health -= 1;
        regenerationCooldown = timeBeforeRegeneration;
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

        yield return new WaitForSeconds(invulnerabilityTime);

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
