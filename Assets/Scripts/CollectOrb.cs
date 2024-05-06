using UnityEngine;

public class CollectOrb : MonoBehaviour
{
    public int experiencePerOrb = 5; // Amount of experience gained per orb
    public float followSpeed = 5.0f; // Speed at which the orb follows the player
    public float followDistance = 0.5f; // Distance at which the orb is considered collected
    public AudioClip collectSound; // Sound to play when the orb is collected

    private bool isFollowing = false; // Flag to determine if the orb is following the player
    private Transform playerTransform; // Reference to the player's transform
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // If the orb is following the player, move it towards the player
        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // Find the player's transform
            playerTransform = other.transform;

            // Set the flag to start following the player
            isFollowing = true;
        }
    }

    private void FollowPlayer()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // If the orb is close enough to the player, consider it collected
        if (distanceToPlayer <= followDistance)
        {
            // Apply the orb's effect to the player
            ApplyOrbEffectToPlayer();

            // Destroy the orb or deactivate it
            Destroy(gameObject);
        }
        else
        {
            // Move the orb towards the player smoothly
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
        }
    }

    private void ApplyOrbEffectToPlayer()
    {
        // Apply the experience gain to the player
        PlayerStats stats = playerTransform.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.GivePlayerExperience(experiencePerOrb);
            // Add any other effects or visual feedback here
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
    }
}
