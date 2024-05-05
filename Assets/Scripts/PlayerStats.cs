using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int experience = 0; 
    public int experienceToLevel = 100;
    public int level = 1;
    public bool levelingUp = false;

    public void Update()
    {
        if (experience >= experienceToLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    ///  Adds a given amount of player experience to the Experience field. 
    /// </summary>
    public void GivePlayerExperience(int amount) 
    {
        experience += amount;
        Debug.Log($"Gave player {amount} exp");
    }

    public void LevelUp() 
    {
        Debug.Log($"Leveled Up! -> {level}");

        experience = 0;
        levelingUp = true;

        GameObject ui = GameObject.FindGameObjectWithTag("UICanvas");
        ScreenOverlay screenOverlay = ui.GetComponent<ScreenOverlay>();
        screenOverlay.GeneratePowerups();
    }
}
