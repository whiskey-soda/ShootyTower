using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseClass_Enemy : MonoBehaviour
{

    public List<heightLevel> heightLevelList = new List<heightLevel>();
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float damagePerSec;

    private void Awake()
    {
        currentHealth = maxHealth;
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
        currentHealth -= damageValue;

        if (currentHealth <=0)
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
