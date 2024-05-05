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

public abstract class BaseUpgrade
{
    public int Id {get;}
    public string Name {get;}
    public Rarity Rarity {get;}

    protected BaseUpgrade(int id, string name, Rarity rarity)
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
public class ScalingUpgrade : BaseUpgrade 
{
    public float Modifier {get;}

    public ScalingUpgrade(int id, string name, Rarity rarity, float modifier) : base(id, name, rarity) 
    {
        this.Modifier = modifier;
    }
}

public class WeaponUpgrade : BaseUpgrade 
{
    public int Modifier {get;}

    public WeaponUpgrade(int id, string name, Rarity rarity, int modifier) : base(id, name, rarity) 
    {
        this.Modifier = modifier;
    }
}

public static class UpgradeGenerator
{
    public static List<BaseUpgrade> GenerateDamageModifiers() 
    {
        return new List<BaseUpgrade>
        {
            new ScalingUpgrade(100, "Damage Modifier +20%", Rarity.COMMON, 1.2f),
            new ScalingUpgrade(101, "Damage Modifier +30%", Rarity.UNCOMMON, 1.3f),
            new ScalingUpgrade(102, "Damage Modifier +40%", Rarity.RARE, 1.4f),
            new ScalingUpgrade(103, "Damage Modifier +50%", Rarity.EPIC, 1.5f),
        };
    }

    public static List<BaseUpgrade> GenerateSpeedModifiers() 
    {
        return new List<BaseUpgrade>
        {
            new ScalingUpgrade(104, "Speed Modifier +20%", Rarity.COMMON, 1.2f),
            new ScalingUpgrade(105, "Speed Modifier +30%", Rarity.UNCOMMON, 1.3f),
            new ScalingUpgrade(106, "Speed Modifier +40%", Rarity.RARE, 1.4f),
            new ScalingUpgrade(107, "Speed Modifier +50%", Rarity.EPIC, 1.5f),
        };
    }

    public static List<BaseUpgrade> GenerateSplitModifiers() 
    {
        return new List<BaseUpgrade>
        {
            new WeaponUpgrade(108, "Split Projectile +1", Rarity.COMMON, 1),
            new WeaponUpgrade(109, "Split Projectile +2", Rarity.UNCOMMON, 2),
            new WeaponUpgrade(110, "Split Projectile +3", Rarity.RARE, 3),
            new WeaponUpgrade(111, "Split Projectile +4", Rarity.EPIC, 4),
        };
    }

    public static List<BaseUpgrade> GenerateBurstModifiers() 
    {
        return new List<BaseUpgrade>
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
    public static List<BaseUpgrade> GenerateUpgrades()
    {
        var outList = new List<BaseUpgrade>();

        outList.AddRange(GenerateDamageModifiers());
        outList.AddRange(GenerateSpeedModifiers());

        outList.AddRange(GenerateBurstModifiers());
        outList.AddRange(GenerateSplitModifiers());

        return outList;
    }

    public static BaseUpgrade GenerateRandomUpgrade()
    {
        var upgrades = GenerateUpgrades();
        return upgrades[new System.Random().Next(upgrades.Count)];
    }

    // Extracts the power type by its rarity.
    public static BaseUpgrade GenerateUpgradeById(int id) 
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
