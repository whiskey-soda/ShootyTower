using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : TowerLayer
{
    [Header("CONFIG")]
    public float spikeDPS = 1.5f;
    public float maxDurabilityLevel = 3;
    public float durabilityDamagedThreshold = 1.5f;
    public float durabilityDestroyedThreshold = 0;
    public float durabilityDamagePerDamageDealt = .5f;
    public Sprite intactSprite;
    public Sprite damagedSprite;
    public Sprite destroyedSprite;
    public SpriteRenderer mySpriteRenderer;

    [Header("DEBUG")]
    public bool isActive = false;
    public float currentDurabilityLevel;
    public bool isDamaged = false;
    public bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        currentDurabilityLevel = maxDurabilityLevel;
        if (mySpriteRenderer == null)
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }

        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //update states based on durability level
        if (currentDurabilityLevel < durabilityDamagedThreshold)
        {
            isDamaged = true;
            isActive = true;

            if (currentDurabilityLevel < durabilityDestroyedThreshold)
            {
                isDestroyed = true;
                isActive = false;
            }
        }
        else
        {
            isDamaged = false;
            isDestroyed = false;
            isActive = true;
        }

        //update sprite based on durability
        if (isDamaged)
        {
            if (isDestroyed)
            {
                mySpriteRenderer.sprite = destroyedSprite;
            }
            else
            {
                mySpriteRenderer.sprite = damagedSprite;
            }
        }
        else
        {
            mySpriteRenderer.sprite = intactSprite;
        }
    }

    /// <summary>
    /// deals damage to adjacent enemies and lowers self durability
    /// </summary>
    /// <param name="enemyHealthScript"></param>
    void Stab(EnemyHealth enemyHealthScript)
    {
        enemyHealthScript.currentHealth -= spikeDPS * Time.deltaTime;

        currentDurabilityLevel -= durabilityDamagePerDamageDealt * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            //if enemy collides with spikes,
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth healthScript = collision.gameObject.GetComponent<EnemyHealth>();
                Stab(healthScript);
            }
        }
    }

    public void repairSpikes()
    {
        currentDurabilityLevel = maxDurabilityLevel;
    }

}
