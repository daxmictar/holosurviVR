using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadowRotate : MonoBehaviour
{
    private Transform _camera; // Reference to the main camera
    private Quaternion _initialRotation;

    private void Start()
    {
        // Store the initial rotation of the sprite
        _initialRotation = transform.rotation;

        // Automatically find the main camera and assign it to the _camera field
        if (Camera.main != null)
        {
            _camera = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Main Camera not found. Ensure a main camera exists in the scene.");
        }
    }

    private void LateUpdate()
    {
        if (_camera != null)
        {
            // Get the direction from the sprite to the camera
            Vector3 directionToCamera = _camera.position - transform.position;

            // Flatten the direction vector on the Y-axis (up direction)
            directionToCamera.y = 0;

            // Face the sprite towards the camera on the XZ plane
            transform.rotation = Quaternion.LookRotation(directionToCamera);

            // Keep the initial rotation on the X-axis
            transform.eulerAngles = new Vector3(_initialRotation.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
