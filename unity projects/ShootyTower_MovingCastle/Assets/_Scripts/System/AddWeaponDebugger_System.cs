using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeaponDebugger_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] WeaponType weapon;
    [SerializeField] HeightLevel height;

    [Header("DEBUG")]
    [SerializeField] WeaponManager_Player weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GameObject.FindObjectOfType<WeaponManager_Player>();
    }

    [ContextMenu("Add Weapon To Player")]
    void AddWeapon()
    {
        weaponManager.AddWeapon(weapon, height);
    }

}
