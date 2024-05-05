using UnityEngine;
// using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    // public PostProcessVolume postProcessVolume;
    // private Vignette vignette;

    // Speed of the vignette intensity change
    // public float lerpSpeed = 2f;

    // Target intensity values
    // private float targetIntensity = 0f;

    void Start()
    {
        // Get the Vignette effect from the Post Processing Volume
        // postProcessVolume.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        // Check if the player is walking (e.g., using input axis or player speed)
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");
        // bool isWalking = horizontalInput != 0 || verticalInput != 0;
        // Set target intensity based on whether the player is walking
        // if (isWalking)
        // {
        //     this.targetIntensity = 0.5f; // Adjust this value as desired for walking vignette
        // }
        // else
        // {
        //     this.targetIntensity = 0f;
        // }
        // Smoothly transition vignette intensity towards the target value
        // vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, lerpSpeed * Time.deltaTime);
    }
}
