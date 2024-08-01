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
    public TextMeshProUGUI description;

    private void Start()
    {
        description = GetComponentInChildren<TextMeshProUGUI>();
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
