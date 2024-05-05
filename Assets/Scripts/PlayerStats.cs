using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Health {get;set;} = 100;
    public int Experience {get;set;} = 0; 
    public int ExperienceToLevel {get;} = 100;
    public int Level {get;set;} = 1;
    public bool LevelingUp {get;set;} = false;

    public void Update()
    {
        if (Experience >= ExperienceToLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    ///  Adds a given amount of player experience to the Experience field. 
    /// </summary>
    public void GivePlayerExperience(int amount) 
    {
        Experience += amount;
        Debug.Log($"Gave player {amount} exp");
    }

    public void LevelUp() 
    {
        Debug.Log($"Leveled Up! -> {Level}");

        Experience = 0;
        LevelingUp = true;

        GameObject ui = GameObject.FindGameObjectWithTag("UICanvas");
        ScreenOverlay screenOverlay = ui.GetComponent<ScreenOverlay>();
        screenOverlay.GeneratePowerups();
    }
}
