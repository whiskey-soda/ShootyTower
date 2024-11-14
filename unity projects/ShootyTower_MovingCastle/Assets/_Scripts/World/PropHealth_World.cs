using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHealth_World : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 200;
    public float currentHealth;
    public bool isDestroyed = false;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damageVal)
    {
        if (!isDestroyed)
        {
            float healthAfterDamage = currentHealth -= damageVal;

            SetCurrentHealth(healthAfterDamage);
        }

    }

    /// <summary>
    /// updates a prop's current health to a new value. if the value is <= 0,
    /// the prop is destroyed
    /// </summary>
    /// <param name="newHealthValue"></param>
    public void SetCurrentHealth(float newHealthValue)
    {
        currentHealth = newHealthValue;

        if (currentHealth <= 0)
        {
            DestroyProp();
        }

    }

    void DestroyProp()
    {
        isDestroyed = true;
        currentHealth = 0;

        //for debugging
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

}
