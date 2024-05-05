using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStatus = player.GetComponent<PlayerStats>();
        
        if (!playerStatus.LevelingUp && isWalking)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
            newPosition.y = posY;
            transform.position = newPosition;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerController")
        {
            var playerScript = other.gameObject.GetComponent<PlayerStats>();
            playerScript.Health -= damage;
            print("Player Health: " + playerScript.Health);

            if (playerScript.Health <= 0) {
                print("You died loser!");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            Destroy(gameObject);
        }

        // print("COLLIDED");
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
