using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeOption_System : MonoBehaviour
{

    WeaponType weaponToUpgrade;
    StatType statToUpgrade;
    UpgradeTier upgradeTier;
    float upgradePercent;
    TextMeshProUGUI description;
    WeaponUpgrader_Player weaponUpgraderScript;

    private void Start()
    {
        description = GetComponentInChildren<TextMeshProUGUI>();
        weaponUpgraderScript = FindObjectOfType<WeaponUpgrader_Player>();
    }

    public void ApplyUpgrade()
    {
        weaponUpgraderScript.UpgradeRangedWeapon(weaponToUpgrade, statToUpgrade, upgradeTier, upgradePercent);
    }

    public void SetUpgradeData(UpgradeGenerator_System.UpgradeOptionData newData)
    {
        weaponToUpgrade = newData.weaponToUpgrade;
        statToUpgrade = newData.statToUpgrade;
        upgradeTier = newData.upgradeTier;
        upgradePercent = newData.upgradePercent;

        if (upgradeTier != UpgradeTier.Legendary)
        {
            description.text =  $"{upgradeTier} Upgrade\n" +
                                $"{weaponToUpgrade}\n" +
                                $"{statToUpgrade} +{upgradePercent}%";
        }
        else
        {
            description.text =  $"{upgradeTier} Upgrade\n" +
                                $"{weaponToUpgrade}\n" +
                                $"{statToUpgrade} x{upgradePercent}";
        }
    }
}
