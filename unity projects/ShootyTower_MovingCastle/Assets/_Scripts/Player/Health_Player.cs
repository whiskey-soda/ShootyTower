using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{

    public static Health_Player instance;

    public float maxHealth;
    public float currentHealth;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageValue)
    {
        currentHealth -= damageValue;

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {

    }

}
