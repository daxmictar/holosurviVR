using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class HunterLogic : MonoBehaviour
{
    private Transform playerPosition;
    public float moveSpeed = 2.0f;
    // @TODO(Dax): Deprecate this? 
    public bool isWalking = true;
    public int damage = 1;
    private Animator animator;
    public float posY = 2f;
    public int health = 1;

    void Start()
    {
        playerPosition = FindPlayer().transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (isWalking)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
            newPosition.y = posY;
            transform.position = newPosition;
        }
        else
        {
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerController")
        {
            var playerScript = other.gameObject.GetComponent<PlayerStats>();
            playerScript.health -= damage;
            print("Player Health: " + playerScript.health);

            if (playerScript.health <= 0) {
                print("You died loser!");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
