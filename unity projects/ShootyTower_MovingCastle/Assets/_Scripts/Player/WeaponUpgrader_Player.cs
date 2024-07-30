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
    public void UpgradeRangedWeapon(WeaponType weaponToUpgrade, WeaponStat statToUpgrade, float upgradePercent)
    {

        foreach (RangedBaseClass_Weapon weapon in ownedWeapons)
        {
            if (weapon.weaponType == weaponToUpgrade)
            {
                //this method does not currently support changing elements
                UpgradeRangedStat(weapon, statToUpgrade, upgradePercent);
            }
        }
    }

    /// <summary>
    /// changes a specific stat based on which stat was specified in parameters
    /// </summary>
    /// <param name="statToUpgrade"></param>
    /// <param name="upgradePercent"></param>
    /// <param name="weapon"></param>
    private static void UpgradeRangedStat(RangedBaseClass_Weapon weapon, WeaponStat statToUpgrade, float upgradePercent)
    {
        //TODO: Make this upgrade based on a new BaseStat variable for each stat.
        //TODO: also make a separate method for multiplying the current stat value for X tier upgrades
        switch (statToUpgrade)
        {
            case WeaponStat.damage:
                weapon.damage += weapon.damage * (upgradePercent / 100);
                break;

            case WeaponStat.fireRate:
                weapon.fireRate += weapon.fireRate * (upgradePercent / 100);
                break;

            case WeaponStat.projectileSpeed:
                weapon.projectileSpeed += weapon.projectileSpeed * (upgradePercent / 100);
                break;

            case WeaponStat.pierce:
                weapon.pierce += weapon.pierce * (upgradePercent / 100);
                break;

            case WeaponStat.knockback:
                weapon.knockback += weapon.knockback * (upgradePercent / 100);
                break;

            case WeaponStat.range:
                weapon.range += weapon.range * (upgradePercent / 100);
                break;

        }
    }
}
