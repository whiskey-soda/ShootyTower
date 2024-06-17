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

    private void Awake()
    {
        NavMeshAgent navAgentScript = GetComponent<NavMeshAgent>();
        navAgentScript.speed = moveSpeed;
    }


    public void TakeDamage(float damageValue)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //damage player
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health_Player>().takeDamage(damagePerSec * Time.deltaTime);
        }
    }

}
