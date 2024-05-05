using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    public bool splitAttack = false;
    public bool singleAttack = false;
    public bool printing = false;
    private List<Upgrade> currentUpgrades = new List<Upgrade>();

    // Start is called before the first frame update
    void Start()
    {
        player = FindPlayer();
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (printing)
        {
            PrintUpgrades();
        }
    }

    public void UpdatePlayerUpgrades(Upgrade newUpgrade) 
    {
        var playerStats = player.GetComponent<PlayerStats>();

        // Check and then do something with the weapon upgrade modifier.
        if (newUpgrade is WeaponUpgrade wu)
        {
            // Check the player's weapon and then modify it accordingly.
        }

        // Check and then do something with the scaling upgrade modifier.
        if (newUpgrade is ScalingUpgrade su)
        {
            var spawner = GameObject.FindGameObjectWithTag("Spawner");
            var monsters = spawner.GetComponent<MonsterSpawner>().monsters;

            // Iterate through each monster and bump the value of their damage taken modifier.
            foreach (var monster in monsters)
            {
                monster.GetComponent<MonsterStats>().percentDamageTaken *= su.Modifier;
            }
        }

        currentUpgrades.Add(newUpgrade);
    }

    private void PrintUpgrades()
    {
        foreach (var upgrade in currentUpgrades)
        {
            Debug.Log(upgrade);
        }
    }

    private GameObject FindPlayer() {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null) { return null; }
        return playerObject;
    }
}
