using UnityEngine;

public class CollectOrb : MonoBehaviour
{
    public int experiencePerOrb = 5;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.GivePlayerExperience(experiencePerOrb);
                Destroy(gameObject);
            }
        }
    }
}
