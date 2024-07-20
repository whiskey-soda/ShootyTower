using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner_Enemy : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform landingPoint;

    /// <summary>
    /// spawns a basic xp orb at spawn position, and gives it's spawning script a landing position
    /// </summary>
    public void SpawnXP()
    {
        GameObject newXPOrb = Instantiate(PrefabLibrary_XP.Instance.BasicXPOrb, spawnPoint.transform.position, Quaternion.identity);
        newXPOrb.GetComponent<Spawn_XP>().landingPosY = landingPoint.transform.position.y;
    }

}
