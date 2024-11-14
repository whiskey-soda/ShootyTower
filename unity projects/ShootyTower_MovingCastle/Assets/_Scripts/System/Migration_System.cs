using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Migration_System : MonoBehaviour
{

    RegionData_World campaignStartRegionData;
    RegionData_World campaignEndRegionData;

    SpawnDirector_System spawnDirector;
    RegionGeneration_World regionGenerator;

    private void Awake()
    {
        spawnDirector = FindObjectOfType<SpawnDirector_System>();
        regionGenerator = FindObjectOfType<RegionGeneration_World>();
    }


    /// <summary>
    /// returns a region data object with current values from the game
    /// </summary>
    /// <returns></returns>
    RegionData_World FetchRegionData()
    {
        
        RegionData_World currentRegionData = new RegionData_World();

        currentRegionData.spawnsPerSec = spawnDirector.spawnsPerSec;
        currentRegionData.healthMultiplier = spawnDirector.healthMultiplier;
        currentRegionData.spawnRateIncreaseMultiplier = spawnDirector.spawnRateIncreaseMultiplier;
        currentRegionData.healthIncreaseMultiplier = spawnDirector.healthIncreaseMultiplier;
        currentRegionData.worldSize = regionGenerator.worldSize;

        return currentRegionData;
    }

    void LoadRegion(RegionData_World region)
    {

        campaignStartRegionData = region;

    }

    [ContextMenu("Player Death")]
    void PlayerDeath()
    {
        RegionManager_System.instance.regions.Add(campaignStartRegionData);
        Debug.Log(RegionManager_System.instance.regions.Count);
    }

    void Migrate()
    {
        RegionManager_System.instance.regions.Add(campaignStartRegionData);

        campaignEndRegionData = FetchRegionData();
        RegionManager_System.instance.regions.Add(campaignEndRegionData);
    }

    [ContextMenu("Init Region")]
    void InitRegion()
    {
        campaignStartRegionData = FetchRegionData();
    }

}
