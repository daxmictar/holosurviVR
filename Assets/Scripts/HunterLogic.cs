using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HunterLogic : MonoBehaviour
{
    private Transform playerPosition;
    public float moveSpeed = 2.0f;
    // @TODO(Dax): Deprecate this? 
    // private bool isWalking = false;
    public int damage = 1;

    void Start()
    {
        playerPosition = FindPlayer().transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
        newPosition.y = 2f;
        transform.position = newPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerController")
        {
            var playerScript = other.gameObject.GetComponent<PlayerLogic>();
            playerScript.health -= damage;
            print("Player Health: " + playerScript.health);

            if (playerScript.health <= 0) {
                print("You died loser!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            Destroy(gameObject);
        }

        print("COLLIDED");
    }

    private GameObject FindPlayer()
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null)
        {
            return null;
        }

        //Debug.Log("Player Found -> ", target);

        return playerObject;
    }
}
