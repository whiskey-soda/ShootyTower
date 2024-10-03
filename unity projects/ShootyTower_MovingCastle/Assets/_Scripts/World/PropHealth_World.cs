using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHealth_World : MonoBehaviour
{
    [SerializeField] float maxHealth = 200;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageVal)
    {
        currentHealth -= damageVal;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
