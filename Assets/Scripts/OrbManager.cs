using UnityEngine;

public class Orbmanager : MonoBehaviour
{
    public OrbRotate orbRotator; // Reference to the OrbRotate script
    public float spawnInterval = 10f; // Interval between spawning orbs in seconds
    public float despawnInterval = 20f; // Interval between despawning orbs in seconds
    private float spawnTimer; // Timer to track time for spawning
    private float despawnTimer; // Timer to track time for despawning

    void Start()
    {
        spawnTimer = 0f; // Initialize spawn timer
        despawnTimer = 0f; // Initialize despawn timer
        orbRotator = GetComponent<OrbRotate>(); // Assign OrbRotate component reference
    }

    void Update()
    {
        // Update spawn timer
        if (spawnTimer < spawnInterval)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            orbRotator.SpawnOrbs();
            spawnTimer = 0f; // Reset spawn timer

            // Start the despawn timer after spawning orbs
            despawnTimer = 0f;
        }

        // Update despawn timer
        if (despawnTimer < despawnInterval)
        {
            despawnTimer += Time.deltaTime;
        }
        else
        {
            orbRotator.ClearOrbs();
            despawnTimer = 0f; // Reset despawn timer
        }
    }
}
