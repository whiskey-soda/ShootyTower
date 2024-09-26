using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]

public class Tileset_World : ScriptableObject
{

    [SerializeField] List<Tile_World> tiles;
    float totalWeight = 0;

    private void OnEnable()
    {
        totalWeight = CalculateTotalWeight();
        tiles.Sort();
    }

    public Tile_World PickRandomTile()
    {
        float tileToSpawn = Random.Range(0, totalWeight);

        Tile_World chosenTile = null;

        foreach (Tile_World tile in tiles)
        {
            if (tileToSpawn <= tile.spawnWeight)
            {
                chosenTile = tile;
                break;
            }
            else { tileToSpawn -= tile.spawnWeight; }

        }

        return chosenTile;
    }

    public float CalculateTotalWeight()
    {
        float totalWeight = 0;

        foreach (Tile_World tile in tiles)
        {
            totalWeight += tile.spawnWeight;
        }
        return totalWeight;
    }

}
