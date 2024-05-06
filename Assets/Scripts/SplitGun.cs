using UnityEngine;

public class SplitGun : MonoBehaviour
{
    // Reference to the bullet prefab
    [SerializeField] private GameObject bulletPrefab;

    // Spawn point for bullets (e.g., a transform representing the gun barrel)
    [SerializeField] private Transform bulletSpawnPointCenter;
    [SerializeField] private Transform bulletSpawnPointLeft;
    [SerializeField] private Transform bulletSpawnPointRight;

    // Number of bullets per burst
    [SerializeField] private int bulletsPerBurst = 3;

    // Time between shots in a burst (in seconds)
    [SerializeField] private float timeBetweenShots = 0.1f;

    // Time between bursts (in seconds)
    [SerializeField] private float timeBetweenBursts = 3.0f;

    // Bullet speed
    [SerializeField] private float bulletSpeed = 20.0f;

    // Sound to play when shooting
    [SerializeField] private AudioClip shootSound;

    // AudioSource component
    private AudioSource audioSource;

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

        // Get AudioSource component
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
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
        var rightRotate = Quaternion.Euler(0, 0, 0);
        Quaternion bulletRotationCenter = bulletSpawnPointCenter.rotation * rightRotate; 
        Quaternion bulletRotationLeft = bulletSpawnPointLeft.rotation * rightRotate; 
        Quaternion bulletRotationRight = bulletSpawnPointRight.rotation * rightRotate; 

        // Instantiate a bullet at the bullet spawn point with the calculated rotation
        GameObject bulletCenter = Instantiate(bulletPrefab, bulletSpawnPointCenter.position, bulletRotationCenter);
        GameObject bulletLeft = Instantiate(bulletPrefab, bulletSpawnPointLeft.position, bulletRotationLeft);
        GameObject bulletRight = Instantiate(bulletPrefab, bulletSpawnPointRight.position, bulletRotationRight);

        // Access the Rigidbody component of the bullet
        Rigidbody bulletRigidbodyCenter = bulletCenter.GetComponent<Rigidbody>();
        Rigidbody bulletRigidbodyLeft = bulletLeft.GetComponent<Rigidbody>();
        Rigidbody bulletRigidbodyRight = bulletRight.GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (bulletRigidbodyCenter != null && bulletRigidbodyLeft != null && bulletRigidbodyRight != null)
        {
            // Set the bullet's velocity to the forward direction of the bullet spawn point
            // Adjust the speed as needed
            //float bulletSpeed = 20.0f; // Adjust the bullet speed as needed
            bulletRigidbodyCenter.velocity = bulletSpawnPointCenter.forward * bulletSpeed;
            bulletRigidbodyLeft.velocity = bulletSpawnPointLeft.forward * bulletSpeed;
            bulletRigidbodyRight.velocity = bulletSpawnPointRight.forward * bulletSpeed;
        }

        float bulletLifetime = 2.0f;

        Destroy(bulletCenter, bulletLifetime);
        Destroy(bulletLeft, bulletLifetime);
        Destroy(bulletRight, bulletLifetime);

        // Play the shooting sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

}
