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
        overlayImage.SetActive(playerStatus.levelingUp);
        
        // "Level Up!" title
        title.SetActive(playerStatus.levelingUp);
        
        // Descriptions of the power-ups
        upgradeTextOne.SetActive(playerStatus.levelingUp);
        upgradeTextTwo.SetActive(playerStatus.levelingUp);
        upgradeTextThree.SetActive(playerStatus.levelingUp);
    
        selectionBox.SetActive(playerStatus.levelingUp);

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
            playerStatus.levelingUp = false;
        }
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

        upgrades[0] = UpgradeGenerator.GenerateRandomUpgrade();
        upgrades[1] = UpgradeGenerator.GenerateRandomUpgrade();
        upgrades[2] = UpgradeGenerator.GenerateRandomUpgrade();

        upgradeTextOne.GetComponent<TextMeshProUGUI>().text = upgrades[0].Name;
        upgradeTextTwo.GetComponent<TextMeshProUGUI>().text = upgrades[1].Name;
        upgradeTextThree.GetComponent<TextMeshProUGUI>().text = upgrades[2].Name;
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
        var playerPowerups = player.GetComponent<PlayerUpgrades>();

        // Pass the selected power to the PlayerUpgrades script.
        playerPowerups.UpdatePlayerUpgrades(selectedUpgrade);

        // Clear all upgrades after their generation and subsequent selection.
        upgrades[0] = null;
        upgrades[1] = null;
        upgrades[2] = null;
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