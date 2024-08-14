using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnData_Enemy : ScriptableObject, IComparable
{
    public float spawnWeight;
    public List<GameObject> prefabsList;

    public int CompareTo(object obj)
    {
        var a = this;
        var b = obj as SpawnData_Enemy;

        int comparisonResult = 0;

        if (a.spawnWeight < b.spawnWeight)
        {
            comparisonResult = -1;
        }
        else if (a.spawnWeight > b.spawnWeight)
        {
            comparisonResult = 1;
        }

        return comparisonResult;
    }
}
