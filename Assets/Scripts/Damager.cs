using UnityEngine;

public class Damager : MonoBehaviour
{
    public float lifetime = 2.0f; // Lifetime of the orb in seconds
    public int damage = 20; // The damage value used for projetile damage calculations.
    public bool destructible = false;

    void Start()
    {
        if (destructible)
        {
            Destroy(gameObject, lifetime); // Destroy the orb after 'lifetime' seconds
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Monster"))
        {
            DropOrb dropOrb = other.GetComponent<DropOrb>(); // orb drop logic 
            MonsterStats monsterStats = other.GetComponent<MonsterStats>(); // monster stat logic
            ColoredFlash flashEffect = other.GetComponent<ColoredFlash>();
            if (dropOrb != null)
            {
                // check if the monster can take damage
                if (monsterStats != null) 
                {
                    monsterStats.TakeDamage(damage);

                    // Trigger flash effect
                    if (flashEffect != null)
                    {
                        // Flash with random color
                        flashEffect.Flash();
                    }
                }

                if (monsterStats.currentHealth <= 0) 
                {
                    dropOrb.Die();
                }
            } else {
                dropOrb.Die();
                Debug.LogError("Stats do not exists on " + other + " triggering Die().");
            }
        }
    }
}

