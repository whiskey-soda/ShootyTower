using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RegionGeneration_World : MonoBehaviour
{
    [Header("CONFIG")]

    [SerializeField] Tileset_World[] tilesets;

    [SerializeField] uint worldSize = 1;

    [Space]
    [SerializeField] uint worldXSize = 5;
    [SerializeField] uint worldYSize = 3;

    const float tileSize = 25;

    [Header("DEBUG")]

    int seed = 0;

    Tileset_World activeTileset;


    [SerializeField] NavMeshSurface[] navMeshSurfaces;

    GameObject environmentParentObj;

    private void Awake()
    {
        //size world to appropriate dimensions
        worldXSize *= worldSize;
        worldYSize *= worldSize;

    }

    private void Start()
    {
        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
        environmentParentObj = new GameObject("Environment");
    }

    Tileset_World PickRandomTileset(Tileset_World[] tileSets)
    {
        Tileset_World chosenTileset = tileSets[Random.Range(0, tileSets.Count())];
        return chosenTileset;
    }

    void SpawnTile(Tile_World tile, Vector2 spawnLocation)
    {
        GameObject spawnedTile = Instantiate(tile.GetRandomVariant(), environmentParentObj.transform);
        spawnedTile.transform.position = spawnLocation;
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

    [ContextMenu("Generate Region")]
    void GenerateRegion()
    {

        //set seed for world generation
        seed = Random.Range(-9999, 10000);
        Random.InitState(seed);

        //set up variables for tile generation
        activeTileset = PickRandomTileset(tilesets);

        //generate the tiles
        for (int rowsSpawned = 0; rowsSpawned < worldYSize; rowsSpawned++)
        {
            SpawnRowOfTiles((uint)rowsSpawned);
        }

        foreach (NavMeshSurface surface in navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }

    }
    
}
