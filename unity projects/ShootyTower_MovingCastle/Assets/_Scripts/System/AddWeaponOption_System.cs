using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddWeaponOption_System : MonoBehaviour
{

    WeaponType weaponType = WeaponType.Bow;

    TMP_Dropdown dropdown;

    Button button;

    private void Awake()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        button = GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (dropdown.value == 0)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void OnEnable()
    {
        // reset dropdown value to prompt when it is loaded
        dropdown.value = 0;
    }


    public void AddWeapon()
    {
        // subtracting 1 for the SELECT LAYER option
        HeightLevel heightLevel = (HeightLevel)dropdown.value - 1;

        WeaponManager_Player.instance.AddWeapon(weaponType, heightLevel);
    }

    

}
