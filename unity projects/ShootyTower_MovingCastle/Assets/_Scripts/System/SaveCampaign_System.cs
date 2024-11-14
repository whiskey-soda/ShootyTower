using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveCampaign_System : MonoBehaviour
{
    
    public static SaveCampaign_System instance;

    string fileName = "SavedCampaign.json";
    public bool isResumingCampaign = false;

    [System.Serializable]
    class CampaignData
    {
        public int seed;
        public RegionData_World campaignStartRegionData;
        public RegionData_World inProgressRegionData;
        public List<float> propHealths;
        public int currentWave;
    }

    CampaignData savedCampaign;

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

    [ContextMenu("Save Campaign")]
    public void SaveGameState()
    {
        UpdateSaveData();

        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + fileName))
        {
            string json = JsonUtility.ToJson(savedCampaign);
            writer.Write(json);

        }

    }

    /// <summary>
    /// updates local savedata with values from game world
    /// </summary>
    private void UpdateSaveData()
    {
        savedCampaign.seed = RegionGeneration_World.instance.seed;

        savedCampaign.campaignStartRegionData = Migration_System.instance.campaignStartRegionData;

        savedCampaign.inProgressRegionData = Migration_System.instance.FetchRegionData();

        PropHealthManager_World.instance.UpdateHealthList();
        savedCampaign.propHealths = PropHealthManager_World.instance.propHealthList;

        savedCampaign.currentWave = SpawnDirector_System.instance.currentWaveNum;
    }

    public void LoadGameState()
    {
        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + fileName))
        {

            string json = reader.ReadToEnd();
            savedCampaign = JsonUtility.FromJson<CampaignData>(json);

        }

        //set game settings before remaking the world
        RegionManager_System.instance.currentRegion = savedCampaign.inProgressRegionData;
        GameManager_System.ConfigureGameSettings();

        //generate world and set prop healths
        RegionGeneration_World.instance.SetSeed(savedCampaign.seed);
        RegionGeneration_World.instance.GenerateRegion();

        PropHealthManager_World.instance.SetHealthList(savedCampaign.propHealths);
        PropHealthManager_World.instance.UpdateHealthOnProps();



        Migration_System.instance.campaignStartRegionData = savedCampaign.campaignStartRegionData;

        SpawnDirector_System.instance.currentWaveNum = savedCampaign.currentWave;

        GameManager_System.instance.StartEnemySpawning();

    }

}
