using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int experience = 0; 
    public int experienceToLevel = 100;
    public int level = 1;
    public bool levelingUp = false;

    public AudioClip hitSound;
    // public AudioClip levelUpSound; // Sound for leveling up
    public Image damageIndicator;
    private AudioSource audioSource;
    private float indicatorDuration = 2.0f;

    public void Update()
    {
        if (experience >= experienceToLevel)
        {
            LevelUp();
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(false);
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

    // Player takes damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        ShowDamageIndicator();
        PlayHitSound();
    }

    private void ShowDamageIndicator()
    {
        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(true);
            Invoke("HideDamageIndicator", indicatorDuration);
        }
    }

    // Hides indicator after it flashes
    private void HideDamageIndicator()
    {
        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(false);
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
