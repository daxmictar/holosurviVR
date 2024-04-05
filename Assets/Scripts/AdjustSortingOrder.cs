using UnityEngine;

public class AdjustSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        // Ensure sprite renderer and camera are assigned
        if (spriteRenderer == null)
        {
            Debug.LogError("Sprite renderer not found!");
            enabled = false;
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        // Calculate distance from sprite to camera
        float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);

        // Set order in layer based on distance (closer sprites will have higher order)
        spriteRenderer.sortingOrder = Mathf.RoundToInt(mainCamera.nearClipPlane - distanceToCamera);
    }
}
