using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner_System))]
public class SpawnDirector_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float waveDurationSecs = 50;

    [SerializeField] SpawnData_Enemy[] wave1_9NewEnemyOrder;

    [SerializeField] float spawnRateIncreasePerWave = 1.2f;
    [SerializeField] float healthBonusIncreasePerWave = 1.1f;

    [Header("DEBUG")]
    [SerializeField] EnemySpawner_System enemySpawnerScript;

    [SerializeField] int currentWaveNum = 0;

    [SerializeField] float spawnDelay;
    [SerializeField] float currentSpawnCooldown;

    [SerializeField] List<SpawnData_Enemy> enemiesInWave;
    [SerializeField] float waveWeightTotal;

    [SerializeField] float spawnsPerSec = 1;
    [SerializeField] float healthBonusPercent = 0;

    [SerializeField] bool enemiesSpawning = false;

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

        spawnDelay = 1 / spawnsPerSec;
        currentSpawnCooldown = 0;

        waveWeightTotal = 0;
        foreach (SpawnData_Enemy enemy in enemiesInWave)
        {
            waveWeightTotal += enemy.spawnWeight;
        }

    }


    /// <summary>
    /// chooses a random enemy from the enemy types in the current wave,
    /// and spawns a random prefab from it's list of prefabs
    /// </summary>
    void SpawnRandomEnemy()
    {
        float spawnNum = Random.Range(1, waveWeightTotal);
        Debug.Log(spawnNum);

        foreach (SpawnData_Enemy enemy in enemiesInWave)
        {

            if (spawnNum <= enemy.spawnWeight)
            {
                //spawn random prefab from the enemy's list of prefabs
                //for example, spawns a harpy at a random height
                int enemyPrefabToSpawn = Random.Range(0, enemy.prefabsList.Count);
                enemySpawnerScript.SpawnEnemy(enemy.prefabsList[enemyPrefabToSpawn]);

                break;
            }

        }
    }
}
