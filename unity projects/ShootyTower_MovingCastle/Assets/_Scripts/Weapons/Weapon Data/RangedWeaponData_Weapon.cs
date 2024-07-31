using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangedWeaponData_Weapon : ScriptableObject
{
    //already set manually on baseclass_weapon
    //public WeaponType weaponType;
    public float damage;
    public float fireRate; //rounds per second
    public float projectileSpeed;
    public float pierce;
    public float knockback;
    public float range;
    public ElementType element;
    public GameObject projectilePrefab;
}
