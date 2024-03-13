using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 5;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            CurrencyManager.singleton.grantManaFromKill();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("TowerAttack"))
        {
            //subtract attack damage from enemy health
            TowerAtkData atkDataScript = collision.GetComponent<TowerAtkData>();
            currentHealth -= atkDataScript.atkDmg;
            Destroy(collision.gameObject);

        }
    }
}
