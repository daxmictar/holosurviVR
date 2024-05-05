using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlay : MonoBehaviour
{
    private GameObject overlayImage;
    //private GameObject button1;
    //private GameObject button2;
    //private GameObject button3;
    private GameObject selectionBox;
    private GameObject title;
    private GameObject description1;
    private GameObject description2;
    private GameObject description3;
    private int upgradeIndex = 0;

    // This should just work now, you can store anything that inherits from BaseUpgrade
    // like WeaponUpgrade, or ScalingUpgrade
    private BaseUpgrade[] upgrades = new BaseUpgrade[3]; 

    // Start is called before the first frame update
    void Start()
    {
        // They should just be assignable whenever now, you can assign to them
        // before you show the UI to the player.
        /*
        upgrades[0] = new BaseUpgrade();
        upgrades[0].name = "Test Upgrade 1";

        upgrades[1] = new BaseUpgrade();
        upgrades[1].name = "Test Upgrade 2";

        upgrades[2] = new BaseUpgrade();
        upgrades[2].name = "Test Upgrade 3";
        */

        //Background overlay
        overlayImage = GameObject.Find("Image");
        
        //Power-up buttons
        //button1 = GameObject.Find("Button1");
        //button2 = GameObject.Find("Button2");
        //button3 = GameObject.Find("Button3");

        //Title textmesh
        title = GameObject.Find("Title");

        //Power-up Descriptions
        description1 = GameObject.Find("Description1");

        description2 = GameObject.Find("Description2");

        description3 = GameObject.Find("Description3");


        //Selection box
        selectionBox = GameObject.Find("SelectionBox");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerStatus = player.GetComponent<PlayerStats>();

        //Semi-transparent backing for dimming
        overlayImage.SetActive(playerStatus.LevelingUp);
        
        //"Level Up!" title
        title.SetActive(playerStatus.LevelingUp);

        //Buttons for selecting power-ups
        //button1.SetActive(playerStatus.levelingUp);
        //button2.SetActive(playerStatus.levelingUp);
        //button3.SetActive(playerStatus.levelingUp);
        
        //Descriptions of the power-ups
        description1.SetActive(playerStatus.LevelingUp);
        description2.SetActive(playerStatus.LevelingUp);
        description3.SetActive(playerStatus.LevelingUp);
    
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

        //Cursor.visible = (playerStatus.levelingUp);
        //Cursor.lockState = (playerStatus.levelingUp) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void GeneratePowerups() 
    {
        var upgrades = UpgradeGenerator.GenerateUpgrades();

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
        var allUpgrades = UpgradeGenerator.GenerateUpgrades();
        
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

    public void ProcessUpgrade(int upgradeID)
    {
        //Apply the effect of the given upgrade

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