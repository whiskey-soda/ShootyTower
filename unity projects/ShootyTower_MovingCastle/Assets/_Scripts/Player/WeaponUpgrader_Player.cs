using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrader_Player : MonoBehaviour
{

    public List<BaseClass_Weapon> ownedWeapons;

    /// <summary>
    /// upgrades a specified stat on a specified weapon type by a specified percentage
    /// </summary>
    /// <param name="weaponToUpgrade"></param>
    /// <param name="statToUpgrade"></param>
    /// <param name="upgradePercent"></param>
    public void UpgradeRangedWeapon(WeaponType weaponToUpgrade, StatType statToUpgrade, UpgradeTier tier, float upgradePercent)
    {

        foreach (RangedBaseClass_Weapon weapon in ownedWeapons)
        {
            if (weapon.weaponType == weaponToUpgrade)
            {
                //this method does not currently support changing elements
                if (tier == UpgradeTier.Legendary)
                {
                    LegendaryUpgradeRangedStat(weapon, statToUpgrade, upgradePercent);
                }
                else { NormalUpgradeRangedStat(weapon, statToUpgrade, upgradePercent); }
            }
        }
    }

    /// <summary>
    /// changes a specific stat based on which stat was specified in parameters
    /// </summary>
    /// <param name="statToUpgrade"></param>
    /// <param name="upgradePercent"></param>
    /// <param name="weapon"></param>
    private static void NormalUpgradeRangedStat(RangedBaseClass_Weapon weapon, StatType statToUpgrade, float upgradePercent)
    {
        switch (statToUpgrade)
        {
            case StatType.damage:
                weapon.damage += weapon.baseStats.damage * (upgradePercent / 100);
                break;

            case StatType.fireRate:
                weapon.fireRate += weapon.baseStats.fireRate * (upgradePercent / 100);
                break;

            case StatType.projectileSpeed:
                weapon.projectileSpeed += weapon.baseStats.projectileSpeed * (upgradePercent / 100);
                break;

            case StatType.pierce:
                weapon.pierce += weapon.baseStats.pierce * (upgradePercent / 100);
                break;

            case StatType.knockback:
                weapon.knockback += weapon.baseStats.knockback * (upgradePercent / 100);
                break;

            case StatType.range:
                weapon.range += weapon.baseStats.range * (upgradePercent / 100);
                break;

        }
    }

    private static void LegendaryUpgradeRangedStat(RangedBaseClass_Weapon weapon, StatType statToUpgrade, float upgradeCoefficient)
    {
        switch (statToUpgrade)
        {
            case StatType.damage:
                weapon.damage *= upgradeCoefficient;
                break;

            case StatType.fireRate:
                weapon.fireRate *= upgradeCoefficient;
                break;

            case StatType.projectileSpeed:
                weapon.projectileSpeed *= upgradeCoefficient;
                break;

            case StatType.pierce:
                weapon.pierce *= upgradeCoefficient;
                break;

            case StatType.knockback:
                weapon.knockback *= upgradeCoefficient;
                break;

            case StatType.range:
                weapon.range *= upgradeCoefficient;
                break;

        }
    }

}
