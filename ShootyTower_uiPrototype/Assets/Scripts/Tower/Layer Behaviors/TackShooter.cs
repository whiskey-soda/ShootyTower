using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackShooter : TowerLayer
{

    [Header("CONFIG")]
    public uint numOfSpikes = 8;
    public float shotsPerSecond = .5f;
    public float projectileSpeed = 10;
    public float projectileDmg = 2;
    public float tackLifetime = .15f;
    public GameObject projectilePrefab;
    

    [Header("DEBUG")]
    public bool readyToFire = false; //tower spawns not ready to fire
    public float firingDelay;
    public float currentCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        calculateFiringDelay();
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
                currentCooldown = firingDelay;
            }
        }

    }

    void shoot()
    {
        float currentFiringAngle = 0;
        float angleIncrementValue = 360 / numOfSpikes;

        //Debug.Log("angle increments by " + angleIncrementValue);

        for (int i = 0; i < numOfSpikes; i++)
        {
            Quaternion shootingRotation = Quaternion.Euler(0, 0, currentFiringAngle);

            //instantiate projectile at tower position with currentshootdirection rotation
            GameObject myProjectile = Instantiate(projectilePrefab, transform.position, shootingRotation);

            //configure projectile damage data
            TowerAtkData attackDataScript = myProjectile.GetComponent<TowerAtkData>();
            attackDataScript.atkDmg = projectileDmg;
            TackDespawn tackDespawnScript = myProjectile.GetComponent<TackDespawn>();
            tackDespawnScript.maxLifetime = tackLifetime;

            //apply velocity to fire projectile at angle
            Rigidbody2D projectileRb2d = myProjectile.GetComponent<Rigidbody2D>();
            //use trig to get x and y components
            float xVelocity = Mathf.Cos(currentFiringAngle * Mathf.Deg2Rad);
            float yVelocity = Mathf.Sin(currentFiringAngle * Mathf.Deg2Rad);
            projectileRb2d.velocity = new Vector2(xVelocity, yVelocity) * projectileSpeed;

            //update shootdirection angle
            currentFiringAngle += angleIncrementValue;

        }
    }

    /// <summary>
    /// recalculates the tack shooter's firing delay based on shotsPerSecond value
    /// </summary>
    public void calculateFiringDelay()
    {
        //set firing delay based on fire rate
        firingDelay = 1 / shotsPerSecond;
        currentCooldown = firingDelay;
    }
}
