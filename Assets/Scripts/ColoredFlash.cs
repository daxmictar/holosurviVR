using UnityEngine;
using System.Collections;

public class ColoredFlash : MonoBehaviour
{
    public Color flashColor = Color.white; // Color to flash
    public float flashDuration = 0.1f; // Duration of the flash effect
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private Color originalColor; // Original color of the sprite
    private bool isFlashing = false; // Flag to indicate if the sprite is currently flashing

    void Start()
    {
        if (spriteRenderer == null)
        {
            // If SpriteRenderer reference is not set, try to get it from the GameObject
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Store the original color of the sprite
        originalColor = spriteRenderer.color;
    }

    // Method to trigger the flash effect
    public void Flash()
    {
        if (!isFlashing)
        {
            // Start a coroutine to flash the sprite
            StartCoroutine(FlashCoroutine());
        }
    }

    // Coroutine to handle the flash effect
    private IEnumerator FlashCoroutine()
    {
        isFlashing = true;

        // Set the sprite color to the flash color
        spriteRenderer.color = flashColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);

        // Reset the sprite color to its original color
        spriteRenderer.color = originalColor;

        isFlashing = false;
    }
}
