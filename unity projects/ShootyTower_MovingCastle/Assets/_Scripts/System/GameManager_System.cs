using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_System : MonoBehaviour
{

    public static GameManager_System instance;

    [Header("CONFIG")]
    [SerializeField] float firstWaveStartDelay = 10;

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

    private void Start()
    {
        ConfigureGameSettings();
        StartGame();

    }

    private static void ConfigureGameSettings()
    {
        //enemy buffs
        SpawnDirector_System.instance.spawnsPerSec = RegionManager_System.instance.currentRegion.spawnsPerSec;
        SpawnDirector_System.instance.healthMultiplier = RegionManager_System.instance.currentRegion.healthMultiplier;

        //enemy buff increase rates
        SpawnDirector_System.instance.spawnRateIncreaseMultiplier = RegionManager_System.instance.currentRegion.spawnRateIncreaseMultiplier;
        SpawnDirector_System.instance.healthIncreaseMultiplier = RegionManager_System.instance.currentRegion.healthIncreaseMultiplier;

        //region generation stats
        RegionGeneration_World.instance.SetWorldSize(RegionManager_System.instance.currentRegion.worldSize);
    }

    void StartGame()
    {
        RegionGeneration_World.instance.GenerateRegion();

        SpawnDirector_System.instance.StartCampaign();
        SpawnDirector_System.instance.Invoke(nameof(SpawnDirector_System.StartWave), firstWaveStartDelay);
    }

}
