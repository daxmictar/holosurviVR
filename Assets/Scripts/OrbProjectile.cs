using UnityEngine;

public class OrbProjectile : MonoBehaviour
{
    public float lifetime = 5.0f; // Lifetime of the orb in seconds

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the orb after 'lifetime' seconds
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Monster"))
        {
            var monster = other.GetComponent<DropOrb>();  
            if (monster != null)
            {
                monster.Die();
            }
        }
    }
}

