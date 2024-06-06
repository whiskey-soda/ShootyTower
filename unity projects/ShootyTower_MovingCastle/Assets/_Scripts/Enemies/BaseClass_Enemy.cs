using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseClass_Enemy : MonoBehaviour
{

    public List<HeightLevel> heightLevelList = new List<HeightLevel>();
    public float health;
    public float moveSpeed;
    public float damagePerSec;

    private void Awake()
    {
        NavMeshAgent navAgentScript = GetComponent<NavMeshAgent>();
        navAgentScript.speed = moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damageValue)
    {
        health -= damageValue;

        if (health <=0)
        {
            enemyDie();
        }
    }

    void enemyDie()
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
