using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class timerUI : MonoBehaviour
{
    public Text timerText;
    public float startTime = 60f; // Initial time in seconds
    private float currentTime; // Current time left
    private bool timerActive = false;

    private void Start()
    {
        // Initialize timer
        ResetTimer();
        StartTimer();
    }

    private void Update()
    {
        // Update timer only if it's active
        if (timerActive)
        {
            currentTime -= Time.deltaTime;

            // Update UI text
            UpdateTimerUI();

            // Check if time's up
            if (currentTime <= 0f)
            {
                // Time's up, handle accordingly
                HandleTimeUp();
            }
        }
    }

    // Start the timer
    public void StartTimer()
    {
        timerActive = true;
    }

    // Stop the timer
    public void StopTimer()
    {
        timerActive = false;
    }

    // Reset the timer to its initial value
    public void ResetTimer()
    {
        currentTime = startTime;
        UpdateTimerUI();
    }

    // Update the UI text to display the current time
    private void UpdateTimerUI()
    {
        timerText.text = "TIME: " + Mathf.CeilToInt(currentTime).ToString();
    }

    // Handle actions when time is up
    private void HandleTimeUp()
    {
        Debug.Log("Time's up!");
        // You can add your own actions here when the timer reaches zero
        // For example: StopTimer();
#if UNITY_EDITOR
        // Check if the application is running in the Unity Editor
        if (EditorApplication.isPlaying)
        {
            // Exit play mode
            EditorApplication.isPlaying = false;
        }
#endif
    }
}
