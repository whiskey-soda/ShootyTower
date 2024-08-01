using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeaponDebugger_System : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] WeaponType weapon;
    [SerializeField] StatType stat;
    [SerializeField] float upgradePercent;

    [Header("DEBUG")]
    [SerializeField] WeaponUpgrader_Player weaponUpgrader;

    // Start is called before the first frame update
    void Start()
    {
        weaponUpgrader = GameObject.FindObjectOfType<WeaponUpgrader_Player>();
    }

    [ContextMenu("Upgrade Weapon")]
    void AddWeapon()
    {
        weaponUpgrader.UpgradeRangedWeapon(weapon, stat, UpgradeTier.Legendary, upgradePercent);
    }
}
