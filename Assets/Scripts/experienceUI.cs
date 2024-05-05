using UnityEngine;
using UnityEngine.UI;

public class experienceUI : MonoBehaviour
{
    public Text experienceText;
    public PlayerStats playerStats;

    private void Start()
    {
        // Ensure the required components are assigned
        if (experienceText == null)
        {
            Debug.LogError("Experience Text is not assigned.");
            enabled = false;
            return;
        }

        if (playerStats == null)
        {
            Debug.LogError("Player Stats is not assigned.");
            enabled = false;
            return;
        }

        // Update the UI initially
        UpdateExperienceUI();
    }

    private void Update()
    {
        // Update the UI whenever the player gains or loses experience points
        UpdateExperienceUI();
    }

    private void UpdateExperienceUI()
    {
        // Update the text to display the player's experience points
        experienceText.text = "XP: " + playerStats.experience.ToString();
    }
}
