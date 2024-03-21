using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("CONFIG")]
    public float delayBetweenWaves = 7.5f;
    public float spawnDelayMin = .25f;
    public float spawnDelayMax = 1.5f;
    public int maxEnemiesInWave = 9;
    public float xSpawnOffset = 5;
    public GameObject enemyPrefab;
    public Camera gameCamera;
    public float enemySpeedMultiplier = .8f;
    public float enemyHealthMultiplier = .8f;

    [Header("DEBUG")]
    public bool waveRunning = false;
    public bool currentlySpawning = false;
    public bool enemiesRemain;
    public int remainingEnemiesToSpawn;
    public bool readyToSpawn;
    public float currentSpawnCooldown;
    public float currentWaveDelay;
    public float xSpawnDistance;
    public float ySpawnMin;
    public float ySpawnMax;

    // Start is called before the first frame update
    void Start()
    {
        SetSpawnLocationValues();

        PrepareNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveRunning)
        {
            //attempt an enemy spawn
            if (currentlySpawning)
            {
                AttemptEnemySpawn();
            }
            //if wave no more enemies to spawn and no enemies remain alive, prep next wave
            else if (!currentlySpawning && !enemiesRemain)
            {
                PrepareNextWave();
            }
        }
        else
        {
            attemptNextWaveStart();
        }
        

        //scan for enemies remaining                                                                                                                    NewMethod();
        enemiesRemain = DoEnemiesRemain();
    }

    /// <summary>
    /// searches for enemies with enemy tag
    /// </summary>
    /// <returns>true if enemies remain, false if no enemies remain</returns>
    private bool DoEnemiesRemain()
    {
        if (GameObject.FindWithTag("Enemy") != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// if readyToSpawn, spawns enemy and decrements remainingEnemiesToSpawn. else, decrements spawn cooldown
    /// </summary>
    private void AttemptEnemySpawn()
    {
        //spawn enemy if readyToSpawn
        if (readyToSpawn)
        {
            SpawnEnemy();
            readyToSpawn = false;
            remainingEnemiesToSpawn--;
            //reset cooldowns if enemies remain
            if (remainingEnemiesToSpawn >= 1)
            {
                float spawnCooldown = Random.Range(spawnDelayMin, spawnDelayMax);
                currentSpawnCooldown = spawnCooldown;
            }
            //end wave if no enemies remain
            else
            {
                StopSpawning();
            }
        }
        //decrease spawn cooldown if director not ready to spawn an enemy
        else
        {
            currentSpawnCooldown -= Time.deltaTime;
            if (currentSpawnCooldown <= 0)
            {
                readyToSpawn = true;
            }
        }
    }

    /// <summary>
    /// spawns an enemy at a random side and random y value
    /// </summary>
    void SpawnEnemy()
    {
        int spawnSideMultiplier;
        if (Random.value < .5f)
        {
            spawnSideMultiplier = 1;
        }
        else
        {
             spawnSideMultiplier = -1;
        }

        //spawn enemy on random side at random y value
        float xSpawn = spawnSideMultiplier * xSpawnDistance;
        float ySpawn = Random.Range(ySpawnMin, ySpawnMax);

        GameObject enemy = Instantiate(enemyPrefab, new Vector3(xSpawn, ySpawn, 0), Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().maxHealth *= enemyHealthMultiplier;
        enemy.GetComponent<EnemyMovement>().moveSpeed *= enemySpeedMultiplier;
    }

    /// <summary>
    /// flips currentlySpawning bool when no more enemies need to be spawned
    /// </summary>
    void StopSpawning()
    {
        currentlySpawning = false;
    }

    void PrepareNextWave()
    {
        currentWaveDelay = delayBetweenWaves;
        waveRunning = false;
    }

    void attemptNextWaveStart()
    {
        if (currentWaveDelay <= 0)
        {
            beginNextWave();
        }
        else
        {
            currentWaveDelay -= Time.deltaTime;
        }
    }

    /// <summary>
    /// resets variables for next wave
    /// </summary>
    void beginNextWave()
    {
        waveRunning = true;
        currentlySpawning = true;
        maxEnemiesInWave += (int)Random.Range(1, 4);
        enemyHealthMultiplier *= 1.2f;
        enemySpeedMultiplier *= 1.15f;
        remainingEnemiesToSpawn = maxEnemiesInWave;
        readyToSpawn = false;
        currentSpawnCooldown = spawnDelayMax;

    }

    /// <summary>
    /// initializes enemy spawn x and y values based on camera size and position
    /// </summary>
    private void SetSpawnLocationValues()
    {
        ySpawnMax = gameCamera.transform.position.y + gameCamera.orthographicSize;
        ySpawnMin = gameCamera.transform.position.y - gameCamera.orthographicSize;

        //calculate distance for enemies to spawn in x direction without being onscreen
        float camWidth = gameCamera.orthographicSize * gameCamera.aspect;
        xSpawnDistance = gameCamera.transform.position.x + (camWidth / 2) + xSpawnOffset;
    }
}
