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
    public void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector2 spawnLocation = (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
    }
}
