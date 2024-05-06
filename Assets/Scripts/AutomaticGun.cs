using UnityEngine;
using System.Collections.Generic;

public class AutomaticGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private int bulletsPerBurst = 4;
    [SerializeField] private float timeBetweenShots = 0.2f;
    [SerializeField] private float timeBetweenBursts = 3.0f;
    [SerializeField] private float bulletSpeed = 20.0f;
    [SerializeField] private AudioClip shootSound;

    private AudioSource audioSource;
    private float shotTimer;
    private float burstTimer;
    private int currentBurstShotCount;

    private void Start()
    {
        shotTimer = 0f;
        burstTimer = timeBetweenBursts;
        currentBurstShotCount = 0;

        // Get AudioSource
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) // If no AudioSource found, add one
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        burstTimer += Time.deltaTime;

        if (burstTimer >= timeBetweenBursts)
        {
            HandleBurst();
        }
    }

    /// <summary>
    /// Increments the number of bullets per burst by the specified value.
    /// </summary>
    /// <param name="addedNumberOfBullets"></param>
    public void UpgradeBulletsPerBurst(int addedNumberOfBullets)
    {
        bulletsPerBurst += addedNumberOfBullets;
    }

    /// <summary>
    /// Increases the current bullet speed by the specified modifier.
    /// </summary>
    /// <param name="bulletSpeedModifier"> A float value between 0 and 0.5. </param>
    public void UpgradeBulletSpeed(float bulletSpeedModifier)
    {
        bulletSpeed *= bulletSpeedModifier;
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }

    private void HandleBurst()
    {
        if (currentBurstShotCount < bulletsPerBurst)
        {
            shotTimer += Time.deltaTime;

            if (shotTimer >= timeBetweenShots)
            {
                FireGunAtClosestEnemy();

                shotTimer = 0f;
                currentBurstShotCount++;
            }
        }
        else
        {
            currentBurstShotCount = 0;
            burstTimer = 0f;
        }
    }

    /*
    // Method to fire the gun
    private void FireGun()
    {
        // Calculate the rotation for the bullet
        // Start with the rotation of the bullet spawn point and apply a 90 degree rotation around the X-axis
        Quaternion bulletRotation = bulletSpawnPoint.rotation * Quaternion.Euler(0, 90, 0);

        // Instantiate a bullet at the bullet spawn point with the calculated rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);

        // Access the Rigidbody component of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (bulletRigidbody != null)
        {
            // Set the bullet's velocity to the forward direction of the bullet spawn point
            // Adjust the speed as needed
            //float bulletSpeed = 20.0f; // Adjust the bullet speed as needed
            bulletRigidbody.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }

        float bulletLifetime = 2.0f;

        Destroy(bullet, bulletLifetime);
    }
    */

    private void FireGunAtClosestEnemy()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");

        if (enemies.Length == 0)
            return;

        // Find the closest enemy
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        // If an enemy is found, fire at it
        if (closestEnemy != null)
        {
            Quaternion bulletRotation = Quaternion.LookRotation(closestEnemy.transform.position - bulletSpawnPoint.position);
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);

            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bullet.transform.forward * bulletSpeed;
            }

            float bulletLifetime = 2.0f;
            Destroy(bullet, bulletLifetime);

            // Play the shooting sound
            if (shootSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }
    
}
