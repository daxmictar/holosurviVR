using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobWeapon : MonoBehaviour
{
    public string targetObjectName;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public float instantiationInterval = 2.0f;
    public Transform projectilePrefab;
    private Transform targetTransform;
    private Transform launchPosition;
    public string launchObjectName;
    public Transform explosionPrefab;

    public AudioClip fireSound;
    public AudioClip landSound;
    private AudioSource audioSource;

    private List<Coroutine> activeCoroutines = new List<Coroutine>();

    void Awake()
    {
        targetTransform = GameObject.Find(targetObjectName).transform;
        launchPosition = GameObject.Find(launchObjectName).transform;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        StartCoroutine(InstantiateProjectiles());
    }

    IEnumerator InstantiateProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(instantiationInterval);

            Transform newProjectile = Instantiate(projectilePrefab, launchPosition.position, Quaternion.identity);

            // Play fire sound
            PlaySound(fireSound);

            // Start a new coroutine to simulate the projectile's trajectory
            Coroutine newCoroutine = StartCoroutine(SimulateProjectile(newProjectile));
            activeCoroutines.Add(newCoroutine);
        }
    }

    IEnumerator SimulateProjectile(Transform projectile)
    {
        activeCoroutines.Add(StartCoroutine(Simulate(projectile)));

        // Wait until the projectile is destroyed
        yield return new WaitUntil(() => !projectile);

        // Remove the coroutine from the list of active coroutines
        activeCoroutines.RemoveAll(coroutine => coroutine == null);
    }

    IEnumerator Simulate(Transform projectile)
    {
        Vector3 startPos = launchPosition.position;
        Vector3 targetPos = targetTransform.position;

        // Move projectile to the position of throwing object + add some offset if needed.
        projectile.position = startPos;

        // Calculate distance to target
        float target_Distance = Vector3.Distance(projectile.position, targetTransform.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y component of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        projectile.rotation = Quaternion.LookRotation(targetTransform.position - projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

        RaycastHit hit;
        if (Physics.Raycast(projectile.position, Vector3.down, out hit))
        {
            // Instantiate explosion prefab at the collision point
            Instantiate(explosionPrefab, hit.point + new Vector3(0, 0.01f, 0), Quaternion.Euler(90, 0, 0));

            // Play land sound
            PlaySound(landSound);
        }

        // Destroy the projectile after it reaches the target
        Destroy(projectile.gameObject);
    }

    private void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
