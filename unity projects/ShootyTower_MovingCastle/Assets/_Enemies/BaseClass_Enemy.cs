using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass_Enemy : MonoBehaviour
{

    public List<heightLevel> heightLevelList = new List<heightLevel>();
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float damage;

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

    }

}
