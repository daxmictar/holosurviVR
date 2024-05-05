using UnityEngine;

public class DamageTick : MonoBehaviour
{
    private const int BASE_DAMAGE_VALUE = 1; // Very weak base damage.
    public float lifetime = 5.0f; // Lifetime of the orb in seconds
    public float damageInterval = 2.0f; // Time interval between each damage tick
    public int damage = BASE_DAMAGE_VALUE; // The damage value used for projectile damage calculations.

    private float nextDamageTime; // Time to apply the next damage tick

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the orb after 'lifetime' seconds
        nextDamageTime = Time.time + damageInterval; // Set initial time for damage tick
    }

    void Update()
    {
        // Apply damage if it's time for the next damage tick
        if (Time.time >= nextDamageTime)
        {
            ApplyDamage();
            nextDamageTime += damageInterval; // Update time for next damage tick
        }
    }

    void ApplyDamage()
    {
        // Check for monsters in the trigger area
        Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Monster"))
            {
                var monsterStats = col.GetComponent<MonsterStats>();
                var dropOrb = col.GetComponent<DropOrb>();
                var flashEffect = col.GetComponent<ColoredFlash>();

                if (monsterStats != null)
                {
                    // Apply damage to the monster
                    monsterStats.TakeDamage(damage);

                    // Trigger flash effect
                    if (flashEffect != null)
                    {
                        flashEffect.Flash();
                    }

                    // Check if the monster should die
                    if (monsterStats.currentHealth <= 0 && dropOrb != null)
                    {
                        dropOrb.Die();
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Apply immediate damage when the orb hits a monster
        ApplyDamage();
    }
}
