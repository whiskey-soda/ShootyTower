using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(WeaponUpgrader_Player))]

public class WeaponManager_Player : MonoBehaviour
{
    [Header("CONFIG")]
    public List<GameObject> weaponPrefabList;

    [Header("DEBUG")]
    [SerializeField] Transform groundWeaponTransform;
    [SerializeField] Transform ground2WeaponTransform;
    [SerializeField] Transform tallWeaponTransform;
    [SerializeField] Transform veryTallWeaponTransform;
    [SerializeField] Transform aerialWeaponTransform;

    [SerializeField] WeaponUpgrader_Player weaponUpgradeScript;

    private void Awake()
    {
        weaponUpgradeScript = GetComponent<WeaponUpgrader_Player>();
    }

    private void Start()
    {
        groundWeaponTransform = GameObject.FindGameObjectWithTag("Ground Target").transform;
        ground2WeaponTransform = GameObject.FindGameObjectWithTag("Ground 2 Weapon Position").transform;
        tallWeaponTransform = GameObject.FindGameObjectWithTag("Tall Target").transform;
        veryTallWeaponTransform = GameObject.FindGameObjectWithTag("Very Tall Target").transform;
        aerialWeaponTransform = GameObject.FindGameObjectWithTag("Aerial Target").transform;
    }

    public void AddWeapon(WeaponType weaponType, HeightLevel heightLevel)
    {
        GameObject weaponToAdd = FetchWeaponPrefab(weaponType);

        //add weapon as child of player object
        GameObject newWeapon = Instantiate(weaponToAdd, transform);
        BaseClass_Weapon newWeaponScript = newWeapon.GetComponent<BaseClass_Weapon>();

        newWeaponScript.heightLevel = heightLevel;
        newWeapon.name = $"{newWeaponScript.weaponType} ({newWeaponScript.heightLevel})";
        

        SetWeaponPosition(newWeapon, heightLevel);

        //if weapon is using a heightlevel that only matters for weapon positioning (like ground2),
        //change the height level to the correct one for collisions so the projectiles have correct layer
        if (newWeaponScript.heightLevel == HeightLevel.Ground2) { newWeaponScript.heightLevel = HeightLevel.Ground; }

        //add weapon to script on upgrader so it can receive upgrades
        weaponUpgradeScript.ownedWeapons.Add(newWeaponScript);

    }

    private void SetWeaponPosition(GameObject newWeapon, HeightLevel heightLevel )
    {
        Vector2 weaponPosition = Vector2.zero;
        switch (heightLevel)
        {
            case HeightLevel.Ground:
                weaponPosition = groundWeaponTransform.position;
                break;

            case HeightLevel.Ground2:
                weaponPosition = ground2WeaponTransform.position;
                break;

            case HeightLevel.Tall:
                weaponPosition = tallWeaponTransform.position;
                break;

            case HeightLevel.VeryTall:
                weaponPosition = veryTallWeaponTransform.position;
                break;

            case HeightLevel.Aerial:
                weaponPosition = aerialWeaponTransform.position;
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
