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
        var automaticGun = player.GetComponent<AutomaticGun>();
        var splitGun = player.GetComponent<SplitGun>();

        // Check and then do something with the scaling upgrade modifier.
        if (newUpgrade is ScalingUpgrade su)
        {
            // damage type
            if (su.Id == 100)
            {
                var spawner = GameObject.FindGameObjectWithTag("Spawner");
                var monsters = spawner.GetComponent<MonsterSpawner>().monsters;
                var currentMonsters = GameObject.FindGameObjectsWithTag("Monster");

                // Iterate through each monster and bump the value of their damage taken modifier.
                foreach (var monster in currentMonsters)
                {
                    monster.GetComponent<MonsterStats>().percentDamageTaken *= su.Modifier;
                }
                // Do the same for all future ocurrences of monsters.
                foreach (var monster in monsters)
                {
                    monster.GetComponent<MonsterStats>().percentDamageTaken *= su.Modifier;
                }
            }

            if (su.Id == 110)
            {
                if (automaticGun.enabled)
                {
                    automaticGun.UpgradeBulletSpeed(su.Modifier);
                }

                if (splitGun.enabled)
                {
                    splitGun.UpgradeBulletSpeed(su.Modifier);
                }
            }
           
            // Less compute intensive way of doing this is to just modify the damage of the
            // projectile itself for all future projetiles, but this doesn't account for
            // other ability types that don't use a bullet prefab.

            // var bulletPrefab = automaticGun.GetBulletPrefab();
            // var orbProjectile = bulletPrefab.GetComponent<OrbProjectile>();
            // orbProjectile.damage = (int)(orbProjectile.damage * su.Modifier);
        }

        // Check and then do something with the weapon upgrade modifier.
        if (newUpgrade is WeaponUpgrade wu)
        {
            // One is for split gun, the other is for automatic gun.
            if (wu.Id == 120 || wu.Id == 130)
            {
                if (splitGun.enabled)
                {
                    splitGun.UpgradeBulletsPerBurst(wu.Modifier);
                }

                if (automaticGun.enabled)
                {
                    automaticGun.UpgradeBulletsPerBurst(wu.Modifier);
                }
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

    private GameObject FindPlayer() 
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null) { return null; }
        return playerObject;
    }
}
