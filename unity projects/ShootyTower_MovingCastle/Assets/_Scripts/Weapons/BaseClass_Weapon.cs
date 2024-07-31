using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass_Weapon : MonoBehaviour
{
    [Header("DEBUG (BASE CLASS)")]
    public WeaponType weaponType;
    public HeightLevel heightLevel;

    /* modular stats system, scrapped because it would require too many list searches
    public List<WeaponStat> stats;

    [Serializable]
    public struct WeaponStat
    {
        public StatType statType;
        public float value;
        public float baseValue;
    }
    */
}
