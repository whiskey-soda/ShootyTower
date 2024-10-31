using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

[RequireComponent(typeof(CircleCollider2D))]

public class HeavyBallista_Weapon : RangedBaseClass_Weapon
{
    [Header("DEBUG (HEAVY BALLISTA)")]
    [SerializeField] List<Hurtbox_Enemy> enemyList = new List<Hurtbox_Enemy>();
    List<Hurtbox_World> propsInRange = new List<Hurtbox_World>();
    CircleCollider2D myCollider;


    protected override void Awake()
    {
        base.Awake();

        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = range;
    }



    protected override void Shoot()
    {
        //ensure radius is accurate before firing
        myCollider.radius = range;

        if (enemyList.Count != 0)
        {
            Hurtbox_Enemy closestEnemy = GetClosestEnemyInRange();
            CreateProjectile(closestEnemy.transform.position - transform.position);
        }
        //target closest prop if no enemies are in range
        else if (propsInRange.Count != 0)
        {
            Hurtbox_World closestProp = GetClosestPropInRange();
            CreateProjectile(closestProp.transform.position - transform.position);
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

    private Hurtbox_Enemy GetClosestEnemyInRange()
    {
        Hurtbox_Enemy closestEnemy = null;
        float closestEnemyDistance = 0;

        foreach (Hurtbox_Enemy enemy in enemyList)
        {
            if (closestEnemy == null)
            {
                closestEnemy = enemy;
                closestEnemyDistance = Vector2.Distance(transform.position, enemy.transform.position);
            }
            else
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) < closestEnemyDistance)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = Vector2.Distance(transform.position, enemy.transform.position);
                }
            }

        }

        return closestEnemy;
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

}
