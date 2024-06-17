using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager_Player : MonoBehaviour
{

    public List<GameObject> weaponList;

    public void AddWeapon(WeaponType weaponType, HeightLevel heightLevel)
    {
        GameObject weaponToAdd = null;

        //parse list for correct weapon prefab
        foreach (GameObject weapon in weaponList)
        {
            if (weapon.GetComponent<BaseClass_Weapon>().weaponType == weaponType)
            {
                weaponToAdd = weapon;
            }
        }

        //add weapon as child of player object + configure with height data
        GameObject newWeapon = Instantiate(weaponToAdd, transform);
        BaseClass_Weapon newWeaponScript = newWeapon.GetComponent<BaseClass_Weapon>();
        newWeaponScript.heightLevel = heightLevel;

        newWeapon.name = $"{newWeaponScript.weaponType} ({newWeaponScript.heightLevel})";

    }
}
