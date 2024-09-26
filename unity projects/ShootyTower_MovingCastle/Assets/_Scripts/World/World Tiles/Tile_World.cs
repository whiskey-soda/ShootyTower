using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]

public class Tile_World : ScriptableObject, IComparable
{

    [SerializeField] GameObject[] tileVariants;
    public float spawnWeight;

    public int CompareTo(object obj)
    {
        var a = this;
        var b = obj as Tile_World;

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

    public GameObject GetRandomVariant()
    {
        GameObject chosenVariant = tileVariants[UnityEngine.Random.Range(0, tileVariants.Count())];
        return chosenVariant;
    }
}
