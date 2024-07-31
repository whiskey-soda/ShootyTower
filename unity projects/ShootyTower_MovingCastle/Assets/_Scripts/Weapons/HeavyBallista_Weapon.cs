using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

[RequireComponent(typeof(CircleCollider2D))]

public class HeavyBallista_Weapon : RangedBaseClass_Weapon
{
    [Header("DEBUG (HEAVY BALLISTA)")]
    [SerializeField] List<Hurtbox_Enemy> enemyList;


    protected override void Awake()
    {
        base.Awake();

        CircleCollider2D myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = range;
    }

    protected override void Shoot()
    {
        if (enemyList.Count != 0)
        {
            Hurtbox_Enemy highestHealthEnemy = GetClosestEnemyInRange();
            CreateProjectile(highestHealthEnemy.transform.position - transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Hurtbox"))
        {
            Hurtbox_Enemy enemyHurtboxScript =  collision.GetComponent<Hurtbox_Enemy>();

            if (enemyHurtboxScript.myHeightLevel == heightLevel &&
                !enemyList.Contains(enemyHurtboxScript))
            {
                enemyList.Add(enemyHurtboxScript);
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
                }
            }
        }

        return closestEnemy;
    }

}
