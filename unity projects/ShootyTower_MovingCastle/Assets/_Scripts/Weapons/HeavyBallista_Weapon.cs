using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

[RequireComponent(typeof(CircleCollider2D))]

public class HeavyBallista_Weapon : RangedBaseClass_Weapon
{
    [Header("HEAVY BALLISTA")]
    [SerializeField] List<BaseClass_Enemy> enemyList;

    private void Reset()
    {
        //set all defaults
        weaponType = WeaponType.HeavyBallista;
        damage = 15;
        fireRate = .4f;
        projectileSpeed = 15;
        pierce = 1;
        knockback = 3;
        range = 10;
        element = ElementType.None;
    }

    private void Awake()
    {
        CircleCollider2D myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = range;
    }

    protected override void Shoot()
    {
        if (enemyList.Count != 0)
        {
            BaseClass_Enemy highestHealthEnemy = GetHighestHealthEnemyInRange();
            CreateProjectile(highestHealthEnemy.transform.position - transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Hurtbox"))
        {
            BaseClass_Enemy enemyScript =  collision.GetComponentInParent<BaseClass_Enemy>();

            if (enemyScript.heightLevelList.Contains(heightLevel) &&
                !enemyList.Contains(enemyScript))
            {
                enemyList.Add(enemyScript);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Hurtbox"))
        {
            BaseClass_Enemy enemyScript = collision.GetComponentInParent<BaseClass_Enemy>();

            if (enemyList.Contains(enemyScript))
            {
                enemyList.Remove(enemyScript);
            }
        }
    }

    private BaseClass_Enemy GetHighestHealthEnemyInRange()
    {
        BaseClass_Enemy highestHealthEnemy = null;
        foreach (BaseClass_Enemy enemy in enemyList)
        {
            if (highestHealthEnemy == null)
            {
                highestHealthEnemy = enemy;
            }
            else
            {
                if (enemy.health > highestHealthEnemy.health)
                {
                    highestHealthEnemy = enemy;
                }
            }
        }

        return highestHealthEnemy;
    }

}
