using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RegionManager_System : MonoBehaviour
{

    public static RegionManager_System instance;

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
                string json = JsonUtility.ToJson(regionData, true);
                writer.WriteLine(json);
            }

        }

    }

    [ContextMenu("Load Regions")]
    /// <summary>
    /// Overwrites the current list of regions with those from the JSON
    /// </summary>
    void LoadRegionsFromJSON()
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

    }

    public void LoadRegion(RegionData_World regionToLoad)
    {
        currentRegion = regionToLoad;
    }

}
