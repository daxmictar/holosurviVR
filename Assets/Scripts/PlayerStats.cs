using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100; // Added max health
    public int experience = 0;
    public int experienceToLevel = 100;
    public int level = 1;
    public bool levelingUp = false;

    public int damage = 5;

    public Slider healthSlider; // Reference to the health slider UI element
    public Slider expSlider; // Reference to the experience points slider UI element

    void Start()
    {
        // Ensure that the health slider's max value matches the player's max health
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        // Ensure that the exp slider's max value matches the player's experience to level
        if (expSlider != null)
        {
            expSlider.maxValue = experienceToLevel;
            expSlider.value = experience;
        }
    }

    public void Update()
    {
        if (experience >= experienceToLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// Adds a given amount of player experience to the Experience field.
    /// </summary>
    public void GivePlayerExperience(int amount)
    {
        experience += amount;
        Debug.Log($"Gave player {amount} exp");
        UpdateExpBar(); // Update the experience bar when the player gains experience
    }

    public void LevelUp()
    {
        Debug.Log($"Leveled Up! -> {level}");

        UpdateExpBar(); // Update the experience bar when the player gains experience

        // Set the experience bar value to 0
        if (expSlider != null)
        {
            expSlider.value = 0;
        }

        experience = 0;
        levelingUp = true;

        GameObject ui = GameObject.FindGameObjectWithTag("UICanvas");
        ScreenOverlay screenOverlay = ui.GetComponent<ScreenOverlay>();
        screenOverlay.GeneratePowerups();

        level++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            // Decrease player's health when colliding with an enemy
            health -= damage;
            UpdateHealthBar();


            // Check if the player's health reaches zero
            if (health <= 0)
            {
#if UNITY_EDITOR
        // Check if the application is running in the Unity Editor
        if (EditorApplication.isPlaying)
        {
            // Exit play mode
            EditorApplication.isPlaying = false;
        }
#endif
            }
        }
    }

    private void UpdateHealthBar()
    {
        // Update the health slider's value based on the player's current health
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    private void UpdateExpBar()
    {
        // Calculate the progress towards the next level
        float progress = (float)experience / experienceToLevel;

        // Update the exp slider's value based on the progress
        if (expSlider != null)
        {
            expSlider.value = progress * expSlider.maxValue;
        }
    }


}
