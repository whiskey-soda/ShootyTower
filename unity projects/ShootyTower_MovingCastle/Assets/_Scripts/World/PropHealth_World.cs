using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHealth_World : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 200;
    float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damageVal)
    {
        currentHealth -= damageVal;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
