using UnityEngine;

public class AutomaticGun : MonoBehaviour
{
    // Reference to the bullet prefab
    [SerializeField] private GameObject bulletPrefab;

    // Spawn point for bullets (e.g., a transform representing the gun barrel)
    [SerializeField] private Transform bulletSpawnPoint;

    // Number of bullets per burst
    [SerializeField] private int bulletsPerBurst = 4;

    // Time between shots in a burst (in seconds)
    [SerializeField] private float timeBetweenShots = 0.2f;

    // Time between bursts (in seconds)
    [SerializeField] private float timeBetweenBursts = 3.0f;

    // Bullet speed
    [SerializeField] private float bulletSpeed = 20.0f;

    // Timers to control shot and burst timing
    private float shotTimer;
    private float burstTimer;

    // Keeps track of the current burst shot count
    private int currentBurstShotCount;

    private void Start()
    {
        // Initialize the shot and burst timers
        shotTimer = 0f;
        burstTimer = timeBetweenBursts;
        currentBurstShotCount = 0;
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStatus = player.GetComponent<PlayerStats>();
        if(!playerStatus.levelingUp){
            // Increment the burst timer by the time elapsed since the last frame
            burstTimer += Time.deltaTime;

            // Check if it's time for the next burst
            if (burstTimer >= timeBetweenBursts)
            {
                // Handle firing the burst
                HandleBurst();
            }
        }
    }

    private void HandleBurst()
    {
        // If the current burst shot count is less than the bullets per burst, fire a shot
        if (currentBurstShotCount < bulletsPerBurst)
        {
            // Increment the shot timer by the time elapsed since the last frame
            shotTimer += Time.deltaTime;

            // Check if it's time for the next shot in the burst
            if (shotTimer >= timeBetweenShots)
            {
                // Fire the gun
                FireGun();

                // Reset the shot timer and increment the burst shot count
                shotTimer = 0f;
                currentBurstShotCount++;
            }
        }
        else
        {
            // Burst is complete, reset the burst shot count and the burst timer
            currentBurstShotCount = 0;
            burstTimer = 0f;
        }
    }

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

}
