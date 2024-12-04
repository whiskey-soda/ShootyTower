using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddWeaponOptionManager_System : MonoBehaviour
{

    public static AddWeaponOptionManager_System instance;

    public HeightLevel heightLevelToArmNext = HeightLevel.Ground;
    public bool allLayersArmed = false;

    AddWeaponOption_System[] newWeaponOptions;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        newWeaponOptions = GetComponentsInChildren<AddWeaponOption_System>();

        //starts enabled, and disables itself after configuring
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!allLayersArmed)
        {
            // whenever menu is shown, make sure height level is set
            FetchHighestHeightLevel();
        }
        GenerateWeaponOptions();
    }

    /// <summary>
    /// checks the player's weapons to determine which levels can recieve new weapons
    /// </summary>
    void FetchHighestHeightLevel()
    {
        // this if statement ensures that if there are no weapons, the layer to upgrade stays 0

        // if there are not weapons, nothing happens and layer stays at 0
        // if there are weapons, AND not all layers are armed, check for weps
        if (WeaponManager_Player.instance.weaponUpgradeScript.ownedWeapons.Any()
            && !allLayersArmed)
        {

            // checks for highest layer armed
            foreach (BaseClass_Weapon weapon in WeaponManager_Player.instance.weaponUpgradeScript.ownedWeapons)
            {
                if (weapon.heightLevel > heightLevelToArmNext)
                {
                    heightLevelToArmNext = weapon.heightLevel;
                }
            }

            // if all heights are not filled, allow ONLY next height to receive a weapon
            // -1 is added in the if statement to account for 0 index
            if ((int)heightLevelToArmNext < Enum.GetNames(typeof(HeightLevel)).Length -1)
            {
                // make next layer available
                heightLevelToArmNext++;
            }
            else
            {
                allLayersArmed = true;
            }
        }
    }

    void GenerateWeaponOptions()
    {

        int[] weaponIndices = new int[Enum.GetNames(typeof(WeaponType)).Length - 1];
        for (int i = 0; i < weaponIndices.Length; i++)
        {
            weaponIndices[i] = i + 1;
        }

        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < weaponIndices.Length; t++)
        {
            int tmp = weaponIndices[t];
            int r = UnityEngine.Random.Range(t, weaponIndices.Length);
            weaponIndices[t] = weaponIndices[r];
            weaponIndices[r] = tmp;
        }


        for (int i = 0; i < newWeaponOptions.Length; i++)
        {
            Debug.Log($"CONFIGURING OPTION {i}");
            newWeaponOptions[i].SetLabel( ((WeaponType)weaponIndices[i]).ToString() );
            newWeaponOptions[i].weaponType = (WeaponType)weaponIndices[i];
        }
    }

}
