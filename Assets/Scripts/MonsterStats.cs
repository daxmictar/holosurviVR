using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float PercentHealth {get => currentHealth / (float)maxHealth;}
    public float percentDamageTaken = 1.0f;
    public RectTransform healthBar;
    public AudioClip hitSound; // Sound for when the monster takes damage
    public AudioClip deathSound; // Sound for when the monster dies
    private AudioSource audioSource; // AudioSource to play the sound
    private float originalWidth; // this fixes the width of the health bar to whatever is set in the editor.

    void Start()
    {
        if (!healthBar) 
        {
            throw new Exception("Health bar is not set in MonsterStats!");
        }

        currentHealth = maxHealth;

        if (healthBar != null) 
        {
            originalWidth = healthBar.sizeDelta.x; 
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateHealthBar();
    }

    /**
    * Is currently being called from OrbProjectile.cs in order to cause the monster
    * to take damage.
    *
    * @TODO(Dax, Octavio): We need to change the damage check to the monsters rather 
    * than the projectiles.
    */
    public void TakeDamage(int damage)
    {
        currentHealth -= (int)(damage * percentDamageTaken);
        PlayHitSound();
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null) 
        {
            healthBar.sizeDelta = new Vector2(originalWidth * PercentHealth, healthBar.sizeDelta.y);
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void Die()
    {
        PlayDeathSound();
        Destroy(gameObject, deathSound ? deathSound.length : 0f);
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
