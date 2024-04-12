using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int experience = 0; 
    public int experienceToLevel = 100;
    public int level = 100;

    public void Start() 
    {
        // Handle stats at start of game
    } 

    /***
        Adds a given amount of player experience to the Experience field. 
    */
    public void GivePlayerExperience(int amount) 
    {
        if (experience > experienceToLevel)
        {
            Debug.Log("Level Up!");
        }

        Debug.Log("Value of player exp before add: " + experience);
        experience += amount;
        Debug.Log("Value of player exp after add: " + experience);
    }

    public void SetPlayerExperience(int experienceValue) {
        experience = experienceValue;
    }
}
