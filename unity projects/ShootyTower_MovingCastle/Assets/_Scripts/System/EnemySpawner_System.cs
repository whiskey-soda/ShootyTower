using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float spawnDistance = 20;

    [Header("DEBUG")]
    [SerializeField] Transform playerTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<Health_Player>().transform;
    }


    /// <summary>
    /// spawns an enemy prefab at a random point that is a set distance away from the player
    /// </summary>
    /// <param name="enemyPrefab"></param>
    public void SpawnEnemy(GameObject enemyPrefab, float healthBonusMultiplier)
    {
        Vector2 spawnLocation = (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * spawnDistance;
        GameObject newEnemyObject = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

        //boost health
        BaseClass_Enemy enemyBaseClass = newEnemyObject.GetComponent<BaseClass_Enemy>();
        enemyBaseClass.health *= healthBonusMultiplier;

        //give necessary info to respawn script for respawning
        Respawn_Enemy respawnScript = newEnemyObject.GetComponent<Respawn_Enemy>();
        respawnScript.spawnScript = this;
        respawnScript.healthBonusMultiplier = healthBonusMultiplier;
        respawnScript.prefab = enemyPrefab;
    }
}
