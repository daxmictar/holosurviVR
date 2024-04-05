using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public enum DeathAnimation
    {
        EXPLODE,
        RUBBLE,
        FIRE,
    }

    public DeathAnimation deathAnim;

    public GameObject explodeEffect;
    public GameObject rubbleEffect;
    public GameObject fireEffect;

    public bool isDebug=false;
    public bool disableWhenHit = true;

    void Start()
    {
        // N/A
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Spell"))
        {
            if (isDebug)
                Debug.Log("hit target");

            GameObject desiredParticleEffect;
            switch (deathAnim)
            {
                case DeathAnimation.EXPLODE:
                    desiredParticleEffect = explodeEffect;
                break;
                case DeathAnimation.FIRE:
                    desiredParticleEffect = fireEffect;
                break;
                case DeathAnimation.RUBBLE:
                    desiredParticleEffect = rubbleEffect;
                break;
                default:
                    desiredParticleEffect = fireEffect;
                break;
            }

            // Create and enable the explosion particle effect
            var effect = Instantiate(desiredParticleEffect, transform.position, Quaternion.Euler(0, 0, 0));
            effect.SetActive(true);

            if(disableWhenHit)
                //disable the target wall
                gameObject.SetActive(false);
        }
    }
}
