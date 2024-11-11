using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseClass_Enemy : MonoBehaviour
{
    [Header("CONFIG")]
    public HeightLevel baseHeight; //defines collision box location
    public List<HeightLevel> heightLevelList;
    public float health; //health bonus is applies by spawner script
    public float moveSpeed;
    public float damagePerSec;

    public float xpValue;

    [Header("DEBUG")]
    public XPSpawner_Enemy xpSpawner;
    

    [Header("DEBUG")]
    //for animator functionality
    public bool isAttacking = false;

    private void Start()
    {
        xpSpawner = GetComponentInChildren<XPSpawner_Enemy>();
    }

    public void DamageHealth(float damageValue)
    {
        health -= damageValue;

        if (health <=0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        for (int i = 0; i< xpValue; i++)
        {
            xpSpawner.SpawnXP();
        }

        Destroy(gameObject);
    }


}
