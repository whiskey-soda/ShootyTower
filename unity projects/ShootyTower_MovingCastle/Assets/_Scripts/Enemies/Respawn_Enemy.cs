using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(Pathfinding_Enemy))]
public class Respawn_Enemy : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float respawnDistance = 30;

    [Header("DEBUG")]
    //both set by spawner on initial spawn
    public EnemySpawner_System spawnScript;
    public float healthBonusMultiplier;
    public GameObject prefab;

    Transform targetTransform;
    float distanceFromTarget;

    private void Start()
    {
        targetTransform = GetComponent<Pathfinding_Enemy>().target;
    }


    private void FixedUpdate()
    {
        distanceFromTarget = Vector2.Distance(transform.position, targetTransform.position);

        if (distanceFromTarget > respawnDistance)
        {
            spawnScript.SpawnEnemy(prefab, healthBonusMultiplier);

            Destroy(gameObject);
        }

    }

}
