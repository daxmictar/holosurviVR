using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobShadowRotate : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    private Quaternion _initialRotation;

    private void Start()
    {
        // Store the initial rotation of the sprite
        _initialRotation = transform.rotation;
    }

    private void LateUpdate()
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
