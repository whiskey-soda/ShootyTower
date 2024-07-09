using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{

    public static Health_Player Instance;

    public float maxHealth;
    public float currentHealth;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
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
