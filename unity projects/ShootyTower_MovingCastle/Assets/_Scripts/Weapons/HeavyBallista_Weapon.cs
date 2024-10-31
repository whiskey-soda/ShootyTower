using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(LineRenderer))]

public class HeavyBallista_Weapon : RangedBaseClass_Weapon
{
    [Header("CONFIG (HEAVY BALLISTA)")]
    [SerializeField] Color trackingLaserColor = Color.red;
    [SerializeField] float trackingLaserWidth = .1f;

    [Header("DEBUG (HEAVY BALLISTA)")]
    bool targetsInRange = false;
    Transform targetTransform;

    [SerializeField] List<Hurtbox_Enemy> enemyList = new List<Hurtbox_Enemy>();
    List<Hurtbox_World> propsInRange = new List<Hurtbox_World>();
    CircleCollider2D myCollider;

    LineRenderer trackingLaser;


    protected override void Awake()
    {
        base.Awake();

        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = range;

        trackingLaser = GetComponent<LineRenderer>();
        trackingLaser.startColor = trackingLaserColor;
        trackingLaser.endColor = trackingLaserColor;
        trackingLaser.startWidth = trackingLaserWidth;
        trackingLaser.endWidth = trackingLaserWidth;

    }

    protected override void Update()
    {
        /*  NOTE: This code currently parses the lists of targets in range every frame.
         *  This is done in order to set the tracking laser.
         *  If I ever want to remove the tracking laser, this logic should be moved to
         *  the Shoot() method so that it ONLY runs when needed
         */

        //scan for targets
        TryFindTarget();

        //display tracking laser on chosen target
        if (targetsInRange)
        {
            trackingLaser.enabled = true;
            trackingLaser.SetPosition(0, transform.position);
            trackingLaser.SetPosition(1, targetTransform.position);
        }
        else { trackingLaser.enabled = false; }

        base.Update();
    }



    protected override void Shoot()
    {
        
        if (targetsInRange)
        {
            CreateProjectile(targetTransform.position - transform.position);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Hurtbox"))
        {
            Hurtbox_Enemy enemyHurtboxScript = collision.GetComponent<Hurtbox_Enemy>();

            if (enemyHurtboxScript.myHeightLevel == heightLevel &&
                !enemyList.Contains(enemyHurtboxScript))
            {
                enemyList.Add(enemyHurtboxScript);
            }
        }
        else if (collision.CompareTag("Prop Hurtbox"))
        {
            Hurtbox_World propHurtboxScript = collision.GetComponent<Hurtbox_World>();

            if (propHurtboxScript.myHeightLevel == heightLevel &&
                !propsInRange.Contains(propHurtboxScript))
            {
                propsInRange.Add(propHurtboxScript);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Hurtbox"))
        {
            Hurtbox_Enemy enemyScript = collision.GetComponent<Hurtbox_Enemy>();

            if (enemyList.Contains(enemyScript))
            {
                enemyList.Remove(enemyScript);
            }
        }
        else if (collision.CompareTag("Prop Hurtbox"))
        {
            Hurtbox_World propHurtboxScript = collision.GetComponent<Hurtbox_World>();

            if (propsInRange.Contains(propHurtboxScript))
            {
                propsInRange.Remove(propHurtboxScript);
            }
        }
    }

    private Hurtbox_Enemy GetHighestHealthEnemyInRange()
    {
        Hurtbox_Enemy highestHealthEnemy = null;
        float highestEnemyHealth = 0;

        foreach (Hurtbox_Enemy enemy in enemyList)
        {
            if (highestHealthEnemy == null)
            {
                highestHealthEnemy = enemy;
                highestEnemyHealth = enemy.myEnemyScript.health;
            }
            else
            {
                if (enemy.myEnemyScript.health > highestEnemyHealth)
                {
                    highestHealthEnemy = enemy;
                    highestEnemyHealth = enemy.myEnemyScript.health;
                }
            }

        }

        return highestHealthEnemy;
    }

    Hurtbox_World GetClosestPropInRange()
    {
        Hurtbox_World closestProp = null;
        float closestPropDistance = 0;
        foreach(Hurtbox_World prop in propsInRange)
        {
            //if no closest prop has been determined
            if (closestProp == null ||
                //OR prop is closer than previous closest prop
                Vector2.Distance(transform.position, prop.transform.position) < closestPropDistance)
            {
                closestProp = prop;
                closestPropDistance = Vector2.Distance(transform.position, prop.transform.position);
            }
        }

        return closestProp;
    }

    void TryFindTarget()
    {
        //ensure radius is accurate before choosing target
        myCollider.radius = range;

        //find target if target in range
        if (enemyList.Count != 0 ||
            propsInRange.Count != 0)
        {
            targetsInRange = true;
            ChooseTarget();
        }
        else
        {
            targetsInRange = false;
        }

    }

    private void ChooseTarget()
    {
        //parse enemy list for highest health enemy
        if (enemyList.Count != 0)
        {
            targetTransform = GetHighestHealthEnemyInRange().transform;
        }
        //if no enemies in range, parse props list for closest prop
        else if (propsInRange.Count != 0)
        {
            targetTransform = GetClosestPropInRange().transform;
        }
    }
}
