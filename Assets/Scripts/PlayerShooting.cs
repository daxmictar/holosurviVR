using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject orbPrefab; // The orb prefab
    public Transform shootingPoint; // The point from which the orb is shot
    public float shootingForce = 1000f; // Force at which the orb is shot

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button press
        {
            ShootOrb();
        }
    }

    void ShootOrb()
    {
        if (orbPrefab && shootingPoint)
        {
            GameObject orb = Instantiate(orbPrefab, shootingPoint.position, shootingPoint.rotation); // Create the orb at the shooting point
            Rigidbody rb = orb.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(shootingPoint.forward * shootingForce); // Apply force to shoot the orb
            }
        }
    }
}

