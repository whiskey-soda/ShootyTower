using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegionGeneration_World : MonoBehaviour
{

    [SerializeField] Tileset_World[] tilesets;

    uint worldXSize = 5;
    uint worldYSize = 5;
    uint worldSize;

    float tileSize = 25;

    int seed = 0;

    Tileset_World activeTileset;
    float tilesetWeight = 0;


    private void Awake()
    {
        //size world to appropriate dimensions
        worldXSize *= worldSize;
        worldYSize *= worldSize;

        //set seed for world generation
        seed = Random.Range(-9999, 10000);
        Random.InitState(seed);

        activeTileset = PickRandomTileset(tilesets);
        tilesetWeight = activeTileset.CalculateTotalWeight();
    }

    Tileset_World PickRandomTileset(Tileset_World[] tileSets)
    {
        Tileset_World chosenTileset = tileSets[Random.Range(0, tileSets.Count())];
        return chosenTileset;
    }

    void SpawnTile(Tile_World tile, Vector2 spawnLocation)
    {
        Instantiate(tile.GetRandomVariant(), spawnLocation, Quaternion.identity);
    }

    void SpawnRowOfTiles(uint rowsSpawned)
    {
        for (int tilesSpawned = 0; tilesSpawned < worldXSize; tilesSpawned++)
        {
            Vector2 spawnLocation = new Vector2(tilesSpawned * tileSize, rowsSpawned * tileSize);

            Tile_World tile = activeTileset.PickRandomTile();
            SpawnTile(tile, spawnLocation);
        }
    }

    void GenerateWorld()
    {
        for (int rowsSpawned = 0; rowsSpawned < worldYSize; rowsSpawned++)
        {
            SpawnRowOfTiles((uint)rowsSpawned);
        }
    }
    
}
