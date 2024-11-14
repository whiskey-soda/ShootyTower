using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RegionManager_System : MonoBehaviour
{

    public static RegionManager_System instance;

    [Header("CONFIG")]
    [SerializeField] float defaultSpawnsPerSec = 1;
    [SerializeField] float defaultHealthMultiplier = 1;
    [SerializeField] float defaultSpawnRateIncreaseMultiplier = 1.2f;
    [SerializeField] float defaultHealthIncreaseMultiplier = 1.1f;
    [SerializeField] uint defaultWorldSize = 1;

    [Header("DEBUG")]
    public List<RegionData_World> regions = new List<RegionData_World>();

    string fileName = "Regions.json";

    public RegionData_World currentRegion = new RegionData_World();

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Save Regions")]
    /// <summary>
    /// Writes all regions saved in the list to the JSON
    /// </summary>
    public void SaveRegionsToJSON()
    {
        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + fileName))
        {

            foreach (RegionData_World regionData in regions)
            {
                string json = JsonUtility.ToJson(regionData);
                writer.WriteLine(json);
            }

        }

    }

    [ContextMenu("Load Regions")]
    /// <summary>
    /// Overwrites the current list of regions with those from the JSON
    /// </summary>
    public void LoadRegionsFromJSON()
    {
        regions.Clear();

        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + fileName))
        {

            while (!reader.EndOfStream)
            {
                string json = reader.ReadLine();
                regions.Add(JsonUtility.FromJson<RegionData_World>(json));
            }

        }

        if (regions.Count == 0)
        {
            NoRegions();
        }

    }

    /// <summary>
    /// adds new regions based on default values and playerprefs
    /// </summary>
    private void NoRegions()
    {
        AddDefaultRegion();
        AddNewRegionFromPlayerPrefs();

        SaveRegionsToJSON();
        LoadRegionsFromJSON();
    }

    private void AddDefaultRegion()
    {
        RegionData_World defaultRegion = new RegionData_World();
        defaultRegion.spawnsPerSec = defaultSpawnsPerSec;
        defaultRegion.healthMultiplier = defaultHealthMultiplier;
        defaultRegion.spawnRateIncreaseMultiplier = defaultSpawnRateIncreaseMultiplier;
        defaultRegion.healthIncreaseMultiplier = defaultHealthIncreaseMultiplier;
        defaultRegion.worldSize = defaultWorldSize;
        regions.Add(defaultRegion);
    }

    /// <summary>
    /// Creates a new region from values in playerprefs. 
    /// if region is not distinct from the default region, then it is not added.
    /// true is returned if region is added, false if it is not.
    /// </summary>
    /// <returns></returns>
    private bool AddNewRegionFromPlayerPrefs()
    {
        bool regionAdded = false;

        RegionData_World newRegion = new RegionData_World();
        newRegion.spawnsPerSec = PlayerPrefs.GetFloat("Top_SpawnsPerSec", defaultSpawnsPerSec);
        newRegion.healthMultiplier = PlayerPrefs.GetFloat("Top_HealthMultiplier", defaultHealthMultiplier);
        newRegion.spawnRateIncreaseMultiplier = PlayerPrefs.GetFloat("Top_SpawnRateIncreateMultiplier", defaultSpawnRateIncreaseMultiplier);
        newRegion.healthIncreaseMultiplier = PlayerPrefs.GetFloat("Top_HealthIncreaseMultiplier", defaultHealthIncreaseMultiplier);
        newRegion.worldSize = (uint)PlayerPrefs.GetInt("Top_WorldSize", (int)defaultWorldSize);

        //if new region is different from default region, add it
        if (
            newRegion.spawnsPerSec != defaultSpawnsPerSec &&
            newRegion.healthMultiplier != defaultHealthMultiplier &&
            newRegion.spawnRateIncreaseMultiplier != defaultSpawnRateIncreaseMultiplier &&
            newRegion.healthIncreaseMultiplier != defaultHealthIncreaseMultiplier &&
            newRegion.worldSize != defaultWorldSize
            )
        {
            regionAdded = true;

            regions.Add(newRegion);
        }

        return regionAdded;
    }

    public void LoadRegion(RegionData_World regionToLoad)
    {
        currentRegion = regionToLoad;
        SceneChanger_System.instance.GoToGameplay();
    }

}
