using UnityEngine;

public class DropOrb : MonoBehaviour
{
    public GameObject orbPrefab;

    public int orbDropCount = 1;

    public AudioClip deathSound; // Sound for when the monster dies

    private Animator animator;

    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Update()
    {
        // n/a for now
    } 

    public void Die() 
    {
        animator.SetBool("playSkelyDie", true);
        DropOrbs();
        BoxCollider enemyCollider = gameObject.GetComponent<BoxCollider>();
        if (enemyCollider != null)
        {
            // Disable the BoxCollider
            enemyCollider.enabled = false;
        }
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject);
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    public void DropOrbs()
    {
        var orbFloatHeight = 0.2f;
        var orbDropPos = new Vector3(transform.position.x, orbFloatHeight, transform.position.z);
        for (int i = 0; i < orbDropCount; i += 1) 
        {
            Instantiate(orbPrefab, orbDropPos, Quaternion.identity);
        }
    }

}
