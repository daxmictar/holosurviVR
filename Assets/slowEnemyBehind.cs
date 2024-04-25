using UnityEngine;

public class slowEnemyBehind : MonoBehaviour
{
    // Reference to the current enemy
    private HunterLogic enemyScript;

    // Slow down enemy when it enters the trigger behind the player
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the enemy
        if (other.gameObject.CompareTag("Monster"))
        {
            enemyScript = other.gameObject.GetComponent<HunterLogic>();

            // Lower the enemy's move speed
            if (enemyScript != null)
            {
                enemyScript.moveSpeed = 0.5f;
            }
        }
    }

    // Restore enemy speed when it exits the trigger
    void OnTriggerExit(Collider other)
    {
        // Check if the exited object is the enemy
        if (other.gameObject.CompareTag("Monster"))
        {
            enemyScript = other.gameObject.GetComponent<HunterLogic>();

            // Restore the enemy's original move speed
            if (enemyScript != null)
            {
                enemyScript.moveSpeed = 2;
            }
        }
    }
}
