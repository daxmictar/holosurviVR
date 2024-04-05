using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WalkTowardsPlayer : MonoBehaviour
{
    public float speed = 1.0f; // The speed of the monster
    public GameObject effect;

    /* Animator vars */
    private Animator anim; // The Animator object to control animation states.
    private bool walking = false; // Flag to keep track of walking.
    private bool scary = false; // Flag to maintain track of the scary animation.
    private bool wasScary = false;
    public float range = 1f;
    private Transform target;  // The target (player) position.

    void Awake() {
        target = FindPlayer().transform;
    }

    void Start() {
        ToggleWalking(true);
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (target != null && walking) {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            var distanceToPlayer = Vector3.Distance(transform.position, target.position); // Check for distances and adjust flags.
            anim.SetFloat("distanceToPlayer", distanceToPlayer);
        }

        
        if (IsNearToPlayer(range) && walking && !wasScary) {
            ToggleWalking(false);
            ToggleScaryAnimation(true);
            wasScary = true;
        }

        if (IsNearToPlayer(range) && !walking && scary) {
            ToggleWalkingWithDelay(true, 3f);
            ToggleScaryAnimationWithDelay(false, 3f);
        }
        
        /*
        if (DistanceToPlayer() < 2f && !scary) {
            ToggleWalking(false);
            ToggleScaryAnimation(true);
            ToggleScaryAnimationWithDelay(false, 1f);
            ToggleWalkingWithDelay(true, 1f);
        }
        */
        LookAtPlayer();
    }

    public bool IsNearToPlayer(float proximity) {
        return DistanceToPlayer() < proximity;
    }

    public float DistanceToPlayer() {
        return Vector3.Distance(transform.position, target.position);
    }

    public void ToggleWalking(bool flag) {
        ToggleWalkingWithDelay(flag, 0);
    }

    public void ToggleWalkingWithDelay(bool flag, float delay) {
        StartCoroutine(WalkingAnimation(flag, delay));
    }

    private IEnumerator WalkingAnimation(bool flag, float delay) {
        // Delay triggering the scary animation.
        yield return new WaitForSeconds(delay);

        anim.SetBool("walking", flag);
        Debug.LogFormat("Walking Animation", flag);
        walking = flag;
    }

    public void ToggleScaryAnimation(bool flag) {
        ToggleScaryAnimationWithDelay(flag, 0);
    }

    public void ToggleScaryAnimationWithDelay(bool flag, float delay) {
        StartCoroutine(ScaryAnimation(flag, delay));
    }

    private IEnumerator ScaryAnimation(bool flag, float delay) {
        // Delay triggering the scary animation.
        yield return new WaitForSeconds(delay);

        anim.SetBool("scary", flag);
        Debug.LogFormat("Scary Animation", flag);
        scary = flag;
    }

    /// <summary>
    /// Find the player's Transform object. 
    /// </summary>
    /// <returns>The GameObject of the player if found else null</returns>
    private GameObject FindPlayer() {
        var playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject == null) {
            return null;
        }

        Debug.Log("Player Found -> ", target);

        return playerObject;
    }

    void LookAtPlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Keep the zombie upright
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Player"){
            Destroy(gameObject);
        }

        print("COLLIDED");
    }
}
