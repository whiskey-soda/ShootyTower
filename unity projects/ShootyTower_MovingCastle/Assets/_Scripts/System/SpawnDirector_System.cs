using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner_System))]
public class SpawnDirector_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float waveDurationSecs = 50;
    [SerializeField] float waveBreakDurationSecs = 5;

    [SerializeField] SpawnData_Enemy[] allEnemyTypesList;

    [SerializeField] SpawnData_Enemy[] wave1_9NewEnemyOrder;

    //applied per wave
    public float spawnRateIncreaseMultiplier = 1.2f;
    public float healthIncreaseMultiplier = 1.1f;

    [SerializeField] float campaignEnd_BuffDecreaseCoefficient = .6f;

    //how much the difficulty buff rates increase after each campaign is done
    [SerializeField] float campaignEnd_DifficultyRatesIncreaseMultiplier = 1.2f;

    [Header("DEBUG")]
    [SerializeField] EnemySpawner_System enemySpawnerScript;

    public int currentWaveNum = 0;

    [SerializeField] bool enemiesSpawning = false;

    [SerializeField] float spawnDelay;
    [SerializeField] float currentSpawnCooldown;

    [Space]
    public float spawnsPerSec = 1;
    public float healthMultiplier = 1;
    public float difficulty = 1;
    [Space]



    [SerializeField] List<SpawnData_Enemy> enemiesInWave;
    [SerializeField] float waveWeightTotal;

    [SerializeField] int numOfEnemyTypesInWave = 3;
    [SerializeField] int wavesRemainingUntilNewType = 3;

    public static SpawnDirector_System instance;


    private void Awake()
    {
        enemySpawnerScript = GetComponent<EnemySpawner_System>();

        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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

        difficulty = (spawnsPerSec + healthMultiplier) / 2;
    }

    [ContextMenu("Start Wave")]
    /// <summary>
    /// configures and starts a new wave, and invokes the method to end the wave after the duration is done
    /// </summary>
    public void StartWave()
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
        Invoke(nameof(StartWave), waveBreakDurationSecs);
    }

    void ConfigureNewWave()
    {
        if (currentWaveNum <= 9)
        {
            enemiesInWave.Clear();
            for (int i = 0; i <= currentWaveNum-1; i++)
            {
                enemiesInWave.Add(wave1_9NewEnemyOrder[i]);
            }
            
        }
        else 
        {
            //waves only get more difficult if you make it past wave 9
            spawnsPerSec *= spawnRateIncreaseMultiplier;
            healthMultiplier *= healthIncreaseMultiplier;

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
        float spawnNum = Random.Range(0, waveWeightTotal);

        foreach (SpawnData_Enemy enemy in enemiesInWave)
        {

            if (spawnNum <= enemy.spawnWeight)
            {
                //spawn random prefab from the enemy's list of prefabs
                //for example, spawns a harpy at a random height
                int enemyPrefabToSpawn = Random.Range(0, enemy.prefabsList.Count);
                enemySpawnerScript.SpawnEnemy(enemy.prefabsList[enemyPrefabToSpawn], healthMultiplier);

                break;
            }
            else
            {
                spawnNum -= enemy.spawnWeight;
            }

        }
    }


    /// <summary>
    /// increases the difficulty scaling multipliers
    /// </summary>
    [ContextMenu("End Campaign")]
    void EndCampaign()
    {
        currentWaveNum = 0;

        // disabled decrementing buffs because the player will always be given the option for an easier region
        /*
        spawnsPerSec = DecrementEnemyBuff(spawnsPerSec, campaignStart_spawnsPerSec);
        healthMultiplier = DecrementEnemyBuff(healthMultiplier, campaignStart_healthBonusMultiplier);
        */

        //NOTE: does this really need to happen? i dont think so.
        spawnRateIncreaseMultiplier *= campaignEnd_DifficultyRatesIncreaseMultiplier;
        healthIncreaseMultiplier *= campaignEnd_DifficultyRatesIncreaseMultiplier;
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
