using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(WeaponUpgrader_Player))]

public class WeaponManager_Player : MonoBehaviour
{
    [Header("CONFIG")]
    public List<GameObject> weaponPrefabList;

    [SerializeField] public WeaponUpgrader_Player weaponUpgradeScript;
    Transform weaponParentTransform;

    public static WeaponManager_Player instance;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        weaponUpgradeScript = GetComponent<WeaponUpgrader_Player>();
        weaponParentTransform = GameObject.FindWithTag("Weapon Parent").transform;

    }


    public void AddWeapon(WeaponType weaponType, HeightLevel heightLevel)
    {
        GameObject weaponToAdd = FetchWeaponPrefab(weaponType);

        //add weapon as child of player object
        GameObject newWeapon = Instantiate(weaponToAdd, weaponParentTransform);
        BaseClass_Weapon newWeaponScript = newWeapon.GetComponent<BaseClass_Weapon>();

        newWeaponScript.heightLevel = heightLevel;
        newWeapon.name = $"{newWeaponScript.weaponType} ({newWeaponScript.heightLevel})";
        

        SetWeaponPosition(newWeapon, heightLevel);

        //add weapon to script on upgrader so it can receive upgrades
        weaponUpgradeScript.ownedWeapons.Add(newWeaponScript);

    }

    private void SetWeaponPosition(GameObject newWeapon, HeightLevel heightLevel )
    {
        Vector2 weaponPosition = Vector2.zero;

        //fetch correct height position from transform library
        switch (heightLevel)
        {
            case HeightLevel.Ground:
                weaponPosition = TransformLibrary_System.instance.GroundWeapon.position;
                break;

            case HeightLevel.Tall:
                weaponPosition = TransformLibrary_System.instance.TallWeapon.position;
                break;

            case HeightLevel.High:
                weaponPosition = TransformLibrary_System.instance.HighWeapon.position;
                break;

            case HeightLevel.Sky:
                weaponPosition = TransformLibrary_System.instance.SkyWeapon.position;
                break;
        }

        newWeapon.transform.position = weaponPosition;
    }

    private GameObject FetchWeaponPrefab(WeaponType weaponType)
    {
        GameObject weaponToAdd = null;

        //parse list for correct weapon prefab
        foreach (GameObject weapon in weaponPrefabList)
        {
            if (weapon.GetComponent<BaseClass_Weapon>().weaponType == weaponType)
            {
                weaponToAdd = weapon;
            }
        }

        return weaponToAdd;
    }
}
