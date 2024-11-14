using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGenerator_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float commonUpgradePercent = 10;
    [SerializeField] float uncommonUpgradePercent = 15;
    [SerializeField] float rareUpgradePercent = 25;
    [SerializeField] float legendaryUpgradePercent = 1.5f;

    [Header("DEBUG")]
    WeaponUpgrader_Player weaponUpgraderScript;
    public UpgradeOption_System[] upgradeOptionScripts;

    public struct UpgradeOptionData
    {
        public WeaponType weaponToUpgrade;
        public StatType statToUpgrade;
        public UpgradeTier upgradeTier;
        public float upgradePercent;
    }

    private void Awake()
    {
        weaponUpgraderScript = FindObjectOfType<WeaponUpgrader_Player>();

        upgradeOptionScripts = FindObjectsOfType<UpgradeOption_System>(true);
    }

    public void GenerateUpgradeOptions()
    {

        UpgradeOptionData[] generatedUpgrades = new UpgradeOptionData[upgradeOptionScripts.Length];

        //generate enough upgrades to fill all the option objects
        for (int i = 0; i < upgradeOptionScripts.Length; i++)
        {
            GenerateUniqueUpgradeOption(generatedUpgrades, i);

            upgradeOptionScripts[i].SetUpgradeData(generatedUpgrades[i]);
        }
    }

    /// <summary>
    /// generates an upgrade and checks if it already exists within the upgrade array.
    /// if it exists already, it is therefore a duplicate and is then regenerated. 
    /// this repeats until an upgrade is generated that does not already exist in the upgrade array.
    /// </summary>
    /// <param name="generatedUpgrades"></param>
    /// <param name="originalIndex"></param>
    private void GenerateUniqueUpgradeOption(UpgradeOptionData[] generatedUpgrades, int originalIndex)
    {
        generatedUpgrades[originalIndex] = GenerateUpgradeOption();

        bool isDuplicate = CheckIfIsDuplicate(generatedUpgrades, originalIndex);

        //if it is a duplicate, generate upgrades until a unique upgrade is generated
        while (isDuplicate)
        {
            generatedUpgrades[originalIndex] = GenerateUpgradeOption();
            isDuplicate = CheckIfIsDuplicate(generatedUpgrades, originalIndex);
        }
    }

    private static bool CheckIfIsDuplicate(UpgradeOptionData[] generatedUpgrades, int originalIndex)
    {
        bool isDuplicate = false;


        //if it is the first upgrade generated, it will always be unique.
        //if it is not the first upgrade, check lower indexes for duplicates.
        if (originalIndex != 0)
        {
            for (int dupeCheckIndex = 0; dupeCheckIndex < originalIndex; dupeCheckIndex++)
            {
                bool isMatch = generatedUpgrades[dupeCheckIndex].weaponToUpgrade == generatedUpgrades[originalIndex].weaponToUpgrade &&
                                generatedUpgrades[dupeCheckIndex].statToUpgrade == generatedUpgrades[originalIndex].statToUpgrade &&
                                generatedUpgrades[dupeCheckIndex].upgradeTier == generatedUpgrades[originalIndex].upgradeTier &&
                                generatedUpgrades[dupeCheckIndex].upgradePercent == generatedUpgrades[originalIndex].upgradePercent;

                //if upgrade is found to already be in the array of upgrades,
                //then the new upgrade is a duplicate and must be re-generated.
                if (isMatch) { isDuplicate = true; }
            }
        }


        return isDuplicate;
    }

    UpgradeOptionData GenerateUpgradeOption()
    {
        WeaponType weaponType;
        StatType statType;
        UpgradeTier upgradeTier;
        float upgradePercent = 0;

        List<WeaponType> upgradeableWeaponTypes = FetchUpgradeableWeaponTypes();
        //select random weapon type from list of upgradeable types
        weaponType = upgradeableWeaponTypes[UnityEngine.Random.Range(0, upgradeableWeaponTypes.Count)];

        //randomly select stat type and upgrade tier from options in the enum
        statType = (StatType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(StatType)).Length);
        upgradeTier = (UpgradeTier)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(UpgradeTier)).Length);

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

        //create upgrade option with generated specs
        UpgradeOptionData generatedUpgrade;
        generatedUpgrade.weaponToUpgrade = weaponType;
        generatedUpgrade.statToUpgrade = statType;
        generatedUpgrade.upgradeTier = upgradeTier;
        generatedUpgrade.upgradePercent = upgradePercent;

        return generatedUpgrade;
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
