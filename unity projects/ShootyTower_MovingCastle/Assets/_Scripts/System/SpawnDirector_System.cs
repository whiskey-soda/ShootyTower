using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner_System))]
public class SpawnDirector_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float waveDurationSecs = 50;

    [SerializeField] SpawnData_Enemy[] allEnemyTypesList;

    [SerializeField] SpawnData_Enemy[] wave1_9NewEnemyOrder;

    //applied per wave
    [SerializeField] float spawnRateIncreaseMultiplier = 1.2f;
    [SerializeField] float healthBonusIncreaseMultiplier = 1.1f;

    [SerializeField] float campaignEnd_BuffDecreaseCoefficient = .6f;

    //how much the difficulty buff rates increase after each campaign is done
    [SerializeField] float campaignEnd_DifficultyRatesIncreaseMultiplier = 1.2f;

    [Header("DEBUG")]
    [SerializeField] EnemySpawner_System enemySpawnerScript;

    [SerializeField] int currentWaveNum = 0;

    [SerializeField] bool enemiesSpawning = false;

    [SerializeField] float spawnDelay;
    [SerializeField] float currentSpawnCooldown;


    [SerializeField] float spawnsPerSec = 1;
    [SerializeField] float healthBonusMultiplier = 1;

    [SerializeField] float campaignStart_spawnsPerSec;
    [SerializeField] float campaignStart_healthBonusMultiplier;


    [SerializeField] List<SpawnData_Enemy> enemiesInWave;
    [SerializeField] float waveWeightTotal;

    [SerializeField] int numOfEnemyTypesInWave = 3;
    [SerializeField] int wavesRemainingUntilNewType = 3;


    private void Awake()
    {
        enemySpawnerScript = GetComponent<EnemySpawner_System>();
    }

    private void FixedUpdate()
    {
        if (enemiesSpawning)
        {
            if (currentSpawnCooldown <= 0)
            {
                SpawnRandomEnemy();
                currentSpawnCooldown = spawnDelay;
            }
            else
            {
                currentSpawnCooldown -= Time.deltaTime;
            }
        }
    }

    [ContextMenu("Start Wave")]
    /// <summary>
    /// configures and starts a new wave, and invokes the method to end the wave after the duration is done
    /// </summary>
    void StartWave()
    {
        currentWaveNum++;

        ConfigureNewWave();

        enemiesSpawning = true;
        Invoke(nameof(EndWave), waveDurationSecs);
    }

    [ContextMenu("End Wave")]
    void EndWave()
    {
        enemiesSpawning = false;
    }

    void ConfigureNewWave()
    {
        if (currentWaveNum <= 9)
        {
            enemiesInWave.Add(wave1_9NewEnemyOrder[currentWaveNum - 1]);
        }
        else 
        {
            spawnsPerSec *= spawnRateIncreaseMultiplier;
            healthBonusMultiplier *= healthBonusIncreaseMultiplier;

            //generate random assortments of enemies for the wave
            //gradually increase the amount of enemies in the wave
            //if the amount of enemies reaches the maximum, stop adding new enemies
            if (numOfEnemyTypesInWave < 9)
            {
                ConfigureRandomWaveComposition();
            }
        }


        enemiesInWave.Sort();

        spawnDelay = 1 / spawnsPerSec;
        currentSpawnCooldown = 0;

        waveWeightTotal = 0;
        foreach (SpawnData_Enemy enemy in enemiesInWave)
        {
            waveWeightTotal += enemy.spawnWeight;
        }

    }

    /// <summary>
    /// processes logic for random wave composition generation
    /// </summary>
    private void ConfigureRandomWaveComposition()
    {
        wavesRemainingUntilNewType--;
        GenerateWaveEnemyTypes();

        if (wavesRemainingUntilNewType <= 0)
        {
            numOfEnemyTypesInWave++;
            wavesRemainingUntilNewType = numOfEnemyTypesInWave;
        }
    }

    /// <summary>
    /// adds new enemies to the wave composition until it is full
    /// </summary>
    private void GenerateWaveEnemyTypes()
    {
        enemiesInWave.Clear();

        for (int i = 0; i < numOfEnemyTypesInWave; i++)
        {
            //add unique enemy type to wave
            bool addedSuccessfully = false;

            while (!addedSuccessfully)
            {
                SpawnData_Enemy enemyToAdd = allEnemyTypesList[Random.Range(0, allEnemyTypesList.Count())];

                if (!enemiesInWave.Contains(enemyToAdd))
                {
                    enemiesInWave.Add(enemyToAdd);
                    addedSuccessfully = true;
                }
            }

        }
    }


    /// <summary>
    /// chooses a random enemy from the enemy types in the current wave,
    /// and spawns a random prefab from it's list of prefabs
    /// </summary>
    void SpawnRandomEnemy()
    {
        float spawnNum = Random.Range(1, waveWeightTotal);

        foreach (SpawnData_Enemy enemy in enemiesInWave)
        {

            if (spawnNum <= enemy.spawnWeight)
            {
                //spawn random prefab from the enemy's list of prefabs
                //for example, spawns a harpy at a random height
                int enemyPrefabToSpawn = Random.Range(0, enemy.prefabsList.Count);
                enemySpawnerScript.SpawnEnemy(enemy.prefabsList[enemyPrefabToSpawn], healthBonusMultiplier);

                break;
            }
            else
            {
                spawnNum -= enemy.spawnWeight;
            }

        }
    }

    [ContextMenu("Start Campaign")]
    void StartCampaign()
    {
        campaignStart_spawnsPerSec = spawnsPerSec;
        campaignStart_healthBonusMultiplier = healthBonusMultiplier;
    }

    /// <summary>
    /// decrements enemy buff values and increases the difficulty scaling multipliers
    /// </summary>
    [ContextMenu("End Campaign")]
    void EndCampaign()
    {
        currentWaveNum = 0;

        spawnsPerSec = DecrementEnemyBuff(spawnsPerSec, campaignStart_spawnsPerSec);
        healthBonusMultiplier = DecrementEnemyBuff(healthBonusMultiplier, campaignStart_healthBonusMultiplier);

        spawnRateIncreaseMultiplier *= campaignEnd_DifficultyRatesIncreaseMultiplier;
        healthBonusIncreaseMultiplier *= campaignEnd_DifficultyRatesIncreaseMultiplier;
    }

    float DecrementEnemyBuff(float buff, float buffBaseValue)
    {
        float newBuffValue = buff;

        newBuffValue *= campaignEnd_BuffDecreaseCoefficient;
        if (newBuffValue < buffBaseValue)
        {
            newBuffValue = buffBaseValue;
        }

        return newBuffValue;
    }

}
