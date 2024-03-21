using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerUpgrade")]
public class SO_TowerUpgrade : ScriptableObject
{
    public enum towerTypeEnum
    {
        TackShooter,
        Sniper,
        Stabber,
        Other,
    }

    public enum towerStatsEnum
    {
        fireRate,
        damage,
        tack_range,
        tack_spikeCount,
        bulletSpeed,
        stabber_durability,
        stabber_repair,
        additional_sniper,
        additional_tack,
        full_heal,
    }

    public float buffValue;
    public towerTypeEnum towerType;
    public towerStatsEnum stat;

    public string upgradeName;
}
