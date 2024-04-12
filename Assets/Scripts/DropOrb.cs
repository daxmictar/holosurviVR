using UnityEngine;

public class DropOrb : MonoBehaviour
{
    public GameObject orbPrefab;

    public int orbDropCount = 1;

    // For testing orb drops
    public bool shouldDie = false;

    public void Update()
    {
        if (shouldDie)
        {
            killMonster();
        }
    } 

    public void Die() 
    {
        DropOrbs();
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

    // For testing purposes!!
    private void killMonster()
    {
        Die();
    }
}
