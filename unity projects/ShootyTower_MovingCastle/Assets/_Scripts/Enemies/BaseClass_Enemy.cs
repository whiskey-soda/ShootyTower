using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseClass_Enemy : MonoBehaviour
{

    public List<HeightLevel> heightLevelList;
    public float health;
    public float moveSpeed;
    public float damagePerSec;

    public float xpValue;
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
