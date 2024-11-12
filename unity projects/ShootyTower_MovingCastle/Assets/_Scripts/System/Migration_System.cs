using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Migration_System : MonoBehaviour
{

    List<RegionData_World> regions;

    RegionData_World campaignStartData;
    RegionData_World campaignEndData;

    SpawnDirector_System spawnDirector;
    RegionGeneration_World regionGenerator;

    string fileName = "Regions.json";

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

    /// <summary>
    /// Writes all regions saved in the list to the JSON
    /// </summary>
    void SaveRegionsToJSON()
    {
        //writes all regions to the json file, one on each line
        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + fileName, append: true))
        {
            foreach(RegionData_World region in regions)
            {
                string json = JsonUtility.ToJson(region);
                writer.WriteLine(json);
            }
        }
    }

    /// <summary>
    /// Overwrites the current list of regions with those from the JSON
    /// </summary>
    void LoadRegionsFromJSON()
    {
        regions.Clear();

        using(StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + fileName))
        {
            while(!reader.EndOfStream)
            {
                string json = reader.ReadLine();
                RegionData_World regionData = JsonUtility.FromJson<RegionData_World>(json);
                regions.Add(regionData);
            }
        }


    }

}
