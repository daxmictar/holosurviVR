using UnityEngine;

public class DropOrb : MonoBehaviour
{
    public GameObject orbPrefab;

    public int orbDropCount = 1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
