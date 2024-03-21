using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : TowerLayer
{

    [Header("CONFIG")]
    public float projectileDmg = 4;
    public float shotsPerSecond = .5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 15;

    [Header("DEBUG")]
    public bool readyToFire = false;
    public float firingDelay;
    public float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        //set firing delay based on fire rate
        firingDelay = 1 / shotsPerSecond;
        currentCooldown = firingDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToFire)
        {
            shoot();
            readyToFire = false;
            currentCooldown = firingDelay;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                readyToFire = true;
                //reset cooldown
                currentCooldown = firingDelay + Random.Range(-.2f, .2f);
            }
        }
    }

    void shoot()
    {
        GameObject currentTarget = null;
        float currentTargetHealth = 0;

        //get all enemies in an array and find the one with the most health
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyArray)
        {
            EnemyHealth enemyHealthScript = enemy.GetComponent<EnemyHealth>();
            if (enemyHealthScript.currentHealth > currentTargetHealth)
            {
                currentTarget = enemy;
                currentTargetHealth = enemyHealthScript.currentHealth;
            }
        }

        //do not spawn bullet if no enemy is found
        if (currentTarget == null)
        {
            return;
        }

        //spawn projectile
        GameObject myProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        //configure projectile damage data
        TowerAtkData attackDataScript = myProjectile.GetComponent<TowerAtkData>();
        attackDataScript.atkDmg = projectileDmg;

        //apply velocity to fire projectile at angle
        Rigidbody2D projectileRb2d = myProjectile.GetComponent<Rigidbody2D>();
        float firingVectorX = currentTarget.transform.position.x - transform.position.x;
        float firingVectorY = currentTarget.transform.position.y - transform.position.y;
        Vector2 firingVector = new Vector2(firingVectorX, firingVectorY);
        firingVector.Normalize();
        projectileRb2d.velocity = firingVector * projectileSpeed;

    }

    /// <summary>
    /// recalculates the sniper's firing delay based on shotsPerSecond value
    /// </summary>
    public void calculateFiringDelay()
    {
        //set firing delay based on fire rate
        firingDelay = 1 / shotsPerSecond;
        currentCooldown = firingDelay;
    }

}
