using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUpgradeHandler_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float commonUpgradePercent = 10;
    [SerializeField] float uncommonUpgradePercent = 15;
    [SerializeField] float rareUpgradePercent = 25;
    [SerializeField] float legendaryUpgradePercent = 1.5f;

    [Header("DEBUG")]
    WeaponUpgrader_Player weaponUpgraderScript;
    

    private void Start()
    {
        weaponUpgraderScript = FindObjectOfType<WeaponUpgrader_Player>();
    }

    void GenerateUpgradeOption()
    {
        WeaponType weaponType;
        StatType statType;
        UpgradeTier upgradeTier;
        float upgradePercent = 0;

        List<WeaponType> upgradeableWeaponTypes = FetchUpgradeableWeaponTypes();
        //select random weapon type from list of upgradeable types
        weaponType = upgradeableWeaponTypes[Random.Range(0, upgradeableWeaponTypes.Count)];

        //randomly select stat type and upgrade tier from options in the enum
        statType = (StatType)Random.Range(0,System.Enum.GetValues(typeof(StatType)).Length);
        upgradeTier = (UpgradeTier)Random.Range(0, System.Enum.GetValues(typeof(UpgradeTier)).Length);

        switch (upgradeTier)
        {
            case UpgradeTier.Common:
                upgradePercent = commonUpgradePercent;
                break;

            case UpgradeTier.Uncommon:
                upgradePercent = uncommonUpgradePercent;
                break;

            case UpgradeTier.Rare:
                upgradePercent = rareUpgradePercent;
                break;

            case UpgradeTier.Legendary:
                upgradePercent = legendaryUpgradePercent;
                break;
        }

    }

    List<WeaponType> FetchUpgradeableWeaponTypes()
    {
        List<WeaponType> upgradeableWeaponTypes = new List<WeaponType>();
        
        foreach (BaseClass_Weapon weapon in weaponUpgraderScript.ownedWeapons)
        {
            if (!upgradeableWeaponTypes.Contains(weapon.weaponType))
            {
                upgradeableWeaponTypes.Add(weapon.weaponType);
            }
        }

        return upgradeableWeaponTypes;
    }

}
