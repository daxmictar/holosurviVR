using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public enum Rarity
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
}

public abstract class Upgrade
{
    public int Id {get;}
    public string Name {get;}
    public Rarity Rarity {get;}

    protected Upgrade(int id, string name, Rarity rarity)
    {
        this.Id = id;
        this.Name = name;
        this.Rarity = rarity;
    }

    override public string ToString()
    {
        return $"Powerup( id={this.Id}, name={this.Name}, rarity={this.Rarity} )";
    }
}

// Uses a float value as its modifier. These types of modifiers are meant to be added to one of the character's stats.
public class ScalingUpgrade : Upgrade 
{
    public float Modifier {get;}

    public ScalingUpgrade(int id, string name, Rarity rarity, float modifier) : base(id, name, rarity) 
    {
        this.Modifier = modifier;
    }
}

public class WeaponUpgrade : Upgrade 
{
    public int Modifier {get;}

    public WeaponUpgrade(int id, string name, Rarity rarity, int modifier) : base(id, name, rarity) 
    {
        this.Modifier = modifier;
    }
}

public static class UpgradeGenerator
{
    public static List<Upgrade> GenerateDamageModifiers() 
    {
        return new List<Upgrade>
        {
            new ScalingUpgrade(100, "Damage Modifier +20%", Rarity.COMMON, 1.2f),
            new ScalingUpgrade(101, "Damage Modifier +30%", Rarity.UNCOMMON, 1.3f),
            new ScalingUpgrade(102, "Damage Modifier +40%", Rarity.RARE, 1.4f),
            new ScalingUpgrade(103, "Damage Modifier +50%", Rarity.EPIC, 1.5f),
        };
    }

    public static List<Upgrade> GenerateSpeedModifiers() 
    {
        return new List<Upgrade>
        {
            new ScalingUpgrade(104, "Speed Modifier +20%", Rarity.COMMON, 1.2f),
            new ScalingUpgrade(105, "Speed Modifier +30%", Rarity.UNCOMMON, 1.3f),
            new ScalingUpgrade(106, "Speed Modifier +40%", Rarity.RARE, 1.4f),
            new ScalingUpgrade(107, "Speed Modifier +50%", Rarity.EPIC, 1.5f),
        };
    }

    public static List<Upgrade> GenerateSplitModifiers() 
    {
        return new List<Upgrade>
        {
            new WeaponUpgrade(108, "Split Projectile +1", Rarity.COMMON, 1),
            new WeaponUpgrade(109, "Split Projectile +2", Rarity.UNCOMMON, 2),
            new WeaponUpgrade(110, "Split Projectile +3", Rarity.RARE, 3),
            new WeaponUpgrade(111, "Split Projectile +4", Rarity.EPIC, 4),
        };
    }

    public static List<Upgrade> GenerateBurstModifiers() 
    {
        return new List<Upgrade>
        {
            new WeaponUpgrade(112, "Burst Projectile +1", Rarity.COMMON, 1),
            new WeaponUpgrade(113, "Burst Projectile +2", Rarity.UNCOMMON, 2),
            new WeaponUpgrade(114, "Burst Projectile +3", Rarity.RARE, 3),
            new WeaponUpgrade(115, "Burst Projectile +4", Rarity.EPIC, 4),
        };
    }

    /// <summary>
    /// Generates a list of ALL possible upgrades.
    /// </summary>
    /// <returns> A List of BaseUpgrades </returns>
    public static List<Upgrade> GenerateUpgrades()
    {
        var outList = new List<Upgrade>();

        outList.AddRange(GenerateDamageModifiers());
        outList.AddRange(GenerateSpeedModifiers());

        outList.AddRange(GenerateBurstModifiers());
        outList.AddRange(GenerateSplitModifiers());

        return outList;
    }

    public static Upgrade GenerateRandomUpgrade()
    {
        var upgrades = GenerateUpgrades();
        return upgrades[new System.Random().Next(upgrades.Count)];
    }

    // Extracts the power type by its rarity.
    public static Upgrade GenerateUpgradeById(int id) 
    {
        var upgrades = GenerateUpgrades();

        var target = upgrades.Find(upgrade => upgrade.Id == id);
        if (target == null) 
        {
            return upgrades.First();
        }

        return target;
    }
}
