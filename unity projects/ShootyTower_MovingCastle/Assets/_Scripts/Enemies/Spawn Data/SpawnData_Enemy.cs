using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnData_Enemy : ScriptableObject
{
    public float spawnWeight;
    public List<GameObject> prefabsList;
}
