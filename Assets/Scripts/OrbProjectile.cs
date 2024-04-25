using UnityEngine;

public class OrbProjectile : MonoBehaviour
{
    private const int BASE_DAMAGE_VALUE = 1; // Very weak base damage.
    public float lifetime = 5.0f; // Lifetime of the orb in seconds
    public int damage = BASE_DAMAGE_VALUE; // The damage value used for projetile damage calculations.

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the orb after 'lifetime' seconds
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Monster"))
        {
            /* 
                @TODO(Dax, Octavio): Unify this behavior into one script so that we don't have to call
                into so many different scripts at runtime?
            */
            var drop_orb = other.GetComponent<DropOrb>(); // orb drop logic 
            var monster_stats = other.GetComponent<MonsterStats>(); // monster stat logic
            if (drop_orb != null)
            {
                // check if the monster can take damage
                if (monster_stats != null) 
                {
                    /* 
                        If it can, then process the damage based on what the projectile has
                        as its damage value.
                    */
                    monster_stats.TakeDamage(damage);
                }

                if (monster_stats.currentHealth <= 0) 
                {
                    drop_orb.Die();
                }
            }   
        }
    }
}

