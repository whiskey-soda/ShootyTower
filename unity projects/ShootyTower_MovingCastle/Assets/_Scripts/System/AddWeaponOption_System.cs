using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AddWeaponOption_System : MonoBehaviour
{

    WeaponType weaponType;


    void AddWeapon(HeightLevel heightLevel)
    {
        WeaponManager_Player.instance.AddWeapon(weaponType, heightLevel);
    }

}
