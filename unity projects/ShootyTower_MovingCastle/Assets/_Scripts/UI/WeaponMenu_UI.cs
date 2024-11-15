using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu_UI : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] GameObject addWeaponPanelPrefab;
    [SerializeField] uint weaponOptionCount = 2;

    void CreateWeaponPanels()
    {
        for (int i = 0; i < weaponOptionCount; i++)
        {
            Instantiate(addWeaponPanelPrefab, transform);
        }
    }

}
