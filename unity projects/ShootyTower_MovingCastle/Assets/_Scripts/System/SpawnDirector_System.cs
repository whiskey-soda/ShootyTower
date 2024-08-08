using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDirector_System : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector2 spawnLocation = Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
    }
}
