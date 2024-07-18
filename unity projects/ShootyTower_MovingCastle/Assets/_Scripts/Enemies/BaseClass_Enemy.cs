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

    [Header("DEBUG")]
    //for animator functionality
    public bool isAttacking = false;


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
        Destroy(gameObject);
    }


}
