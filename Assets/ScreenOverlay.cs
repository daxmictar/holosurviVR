using TMPro;
using UnityEngine;

public class ScreenOverlay : MonoBehaviour
{
    private GameObject title, overlayImage;
    public GameObject selectionBox;
    public GameObject upgradeTextOne, upgradeTextTwo, upgradeTextThree;
    private int upgradeIndex = 0;

    // This should just work now, you can store anything that inherits from BaseUpgrade
    // like WeaponUpgrade, or ScalingUpgrade
    private Upgrade[] upgrades = new Upgrade[3]; 

    // Start is called before the first frame update
    void Start()
    {
        if (!upgradeTextOne || !upgradeTextTwo || !upgradeTextThree)
        {
            Debug.Log("Missing descriptions links for fields in ScreenOverlay.");
        }

        if (!selectionBox)
        {
            Debug.Log("Missing SelectionBox for ScreenOverlay.");
        }

        //Background overlay
        overlayImage = GameObject.Find("Image");

        //Title textmesh
        title = GameObject.Find("Title");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStatus = player.GetComponent<PlayerStats>();

        // Semi-transparent backing for dimming
        overlayImage.SetActive(playerStatus.LevelingUp);
        
        // "Level Up!" title
        title.SetActive(playerStatus.LevelingUp);
        
        // Descriptions of the power-ups
        upgradeTextOne.SetActive(playerStatus.LevelingUp);
        upgradeTextTwo.SetActive(playerStatus.LevelingUp);
        upgradeTextThree.SetActive(playerStatus.LevelingUp);
    
        selectionBox.SetActive(playerStatus.LevelingUp);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upgradeIndex = (upgradeIndex > 0) ? (upgradeIndex - 1) : 2;
            //selectionBox.transform.position = new Vector3(selectionBox.transform.position.x, 70 - (100 * upgradeIndex), selectionBox.transform.position.z);
            print("Up pressed index: " + upgradeIndex);
        } 
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            upgradeIndex = (upgradeIndex < 2) ? (upgradeIndex + 1) : 0;
            //selectionBox.transform.position = new Vector3(selectionBox.transform.position.x, 70 - (100 * upgradeIndex), selectionBox.transform.position.z);
            print("Down pressed index: " + upgradeIndex);
        }

        RectTransform rectTransform = selectionBox.GetComponent<RectTransform>();
        switch(upgradeIndex)
        {
            case 0:
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, 70, rectTransform.localPosition.z);
            break;
            case 1:
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, -30, rectTransform.localPosition.z);
            break;
            case 2:
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, -130, rectTransform.localPosition.z);
            break;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ProcessUpgrade(upgradeIndex);
            playerStatus.LevelingUp = false;
        }
    }

    private void GenerateNewUpgrades()
    {
        upgrades[0] = UpgradeGenerator.GenerateRandomUpgrade();
        upgrades[1] = UpgradeGenerator.GenerateRandomUpgrade();
        upgrades[2] = UpgradeGenerator.GenerateRandomUpgrade();
    }

    private void SetUpgradeText()
    {
        upgradeTextOne.GetComponent<TextMeshProUGUI>().text = upgrades[0].Name;
        upgradeTextTwo.GetComponent<TextMeshProUGUI>().text = upgrades[1].Name;
        upgradeTextThree.GetComponent<TextMeshProUGUI>().text = upgrades[2].Name;
    }

    public void GeneratePowerups() 
    {
        // EXAMPLES for OCTAVIO

        // Generates a random upgrade, use this if you want to generate a SINGLE upgrade.
        // Each upgrade has an Id, a Name, a Rarity, and a Modifer. The Modifier changes
        // based on whether it is a WeaponUpgrade (then it's an int) or a ScalingUpgrade (a float).

        // var randomUpgrade = UpgradeGenerator.GenerateRandomUpgrade();

        // These functions return a List<BaseUpgrade>.         

        // var splitWeaponPowerups = UpgradeGenerator.GenerateSplitUpgrades();
        // var burstWeaponPowerups = UpgradeGenerator.GenerateBurstUpgrades();
        // var damageUpgrades = UpgradeGenerator.GenerateDamageUpgrades();
        // var speedUpgrades = UpgradeGenerator.GenerateSpeedUpgrades();

        // Or you can just generate them all at once.
        // var upgrades = UpgradeGenerator.GenerateUpgrades();

        GenerateNewUpgrades();

        SetUpgradeText();

        foreach (var upgrade in upgrades)
        {
            // Below is how you perform type introspection, which basically just means: 
            // 'figure out what type it is, and then use it as if it were that type'.
            if (upgrade is WeaponUpgrade wu)
            {
                Debug.Log($"This is a WeaponUpgrade object -> {wu.Modifier}");
            }

            if (upgrade is ScalingUpgrade su)
            {
                Debug.Log($"This is a ScalingUpgrade object -> {su.Modifier}");
            }
        }
    }

    /// <summary>
    ///  Apply the effect of the given upgrade
    /// </summary>
    /// <param name="upgradeSelection"> A number between 0 and 2 </param>
    public void ProcessUpgrade(int upgradeSelection)
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (!player)
        {
            Debug.Log("Could not find the `Player` when calling ProcessUpgrade.");
            return;
        }

        var selectedUpgrade = upgrades[upgradeSelection];

        // Check and then do something with the weapon upgrade modifier.
        if (selectedUpgrade is WeaponUpgrade wu)
        {
            var stats = player.GetComponent<PlayerStats>();

            // Check the player's weapon and then modify it accordingly.
        }

        // Check and then do something with the scaling upgrade modifier.
        if (selectedUpgrade is ScalingUpgrade su)
        {
            var spawner = GameObject.FindGameObjectWithTag("Spawner");
            var monsters = spawner.GetComponent<MonsterSpawner>().monsters;

            // Iterate through each monster and bump the value of their damage taken modifier.
            foreach (var monster in monsters)
            {
                monster.GetComponent<MonsterStats>().damageTakenModifier *= su.Modifier;
            }
        }
    }
}

//TODO
//- UI Canvas 
//- Pause Timer
//- Power-Up Menu (Directional Controls)
//- Power-Up Struct Data
//- Power-Up Calculation
//- Melee Weapon
//- Run Stat Tracker
//- 