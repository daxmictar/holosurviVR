using UnityEngine;

public class OrbRotate : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject prefabToOrbit; // Prefab to orbit around the player
    public int numberOfOrbs = 5; // Number of orbs to orbit around the player
    public float orbitRadius = 3f; // Radius of the orbit
    public float orbitSpeed = 3f; // Speed of the orbit

    private GameObject[] orbs; // Array to store the orbs
    private float[] orbitAngles; // Array to store the current orbit angles for each orb

    void Start()
    {
        orbs = new GameObject[numberOfOrbs];
        orbitAngles = new float[numberOfOrbs];

        SpawnOrbs();
    }

    void Update()
    {
        for (int i = 0; i < orbs.Length; i++)
        {
            orbitAngles[i] += orbitSpeed * Time.deltaTime;
            Vector3 orbitPosition = player.position + Quaternion.Euler(0, orbitAngles[i], 0) * Vector3.forward * orbitRadius;
            if (orbs[i]) 
            {
                orbs[i].transform.position = orbitPosition;
            }
        }
    }

    public void SpawnOrbs()
    {
        ClearOrbs();

        for (int i = 0; i < numberOfOrbs; i++)
        {
            float angle = i * (360f / numberOfOrbs);
            Vector3 spawnPosition = player.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * orbitRadius;
            orbs[i] = Instantiate(prefabToOrbit, spawnPosition, Quaternion.identity);
            orbitAngles[i] = angle;
        }
    }

    public void ClearOrbs()
    {
        foreach (GameObject orb in orbs)
        {
            Destroy(orb);
        }
        orbs = new GameObject[numberOfOrbs];
        orbitAngles = new float[numberOfOrbs];
    }
}
