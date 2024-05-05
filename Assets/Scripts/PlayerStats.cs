using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int experience = 0; 
    public int experienceToLevel = 100;
    public int level = 1;
    public bool levelingUp = false;

    public AudioClip hitSound;
    public Image damageIndicator;
    public AudioClip experiencePickupSound;
    private AudioSource audioSource;
    private float indicatorDuration = 2.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(false);

            // Ensure damage indicator is a child of a Canvas set to Screen Space - Overlay
            Canvas canvas = damageIndicator.GetComponentInParent<Canvas>();
            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
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
    ///  Adds a given amount of player experience to the Experience field. 
    /// </summary>
    public void GivePlayerExperience(int amount) 
    {
        experience += amount;
        Debug.Log($"Gave player {amount} exp");
        PlayExperiencePickupSound();
    }

    private void PlayExperiencePickupSound()
    {
        if (experiencePickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(experiencePickupSound);
        }
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
        else
        {
            Debug.LogWarning("Damage indicator is not assigned.");
        }
    }

    private void HideDamageIndicator()
    {
        if (damageIndicator != null)
        {
            Debug.Log("Hiding damage indicator");
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
