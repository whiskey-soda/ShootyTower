using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class KnockbackRegion_Player : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float knockbackForce = 3;
    [SerializeField] float stuckEnemyRange = 1.5f;
    [SerializeField] float unstickEnemyDisplacement = 1;
    [SerializeField] float knockbackDetectionLeeway = .2f;

    [Header("DEBUG")]
    [SerializeField] Movement_Player myMovementScript;
    [SerializeField] Rigidbody2D myRigidbody2D;

    public List<Knockback_Enemy> enemiesInRange;

    private void Start()
    {
        myMovementScript = GetComponentInParent<Movement_Player>();
        myRigidbody2D = GetComponentInParent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (myMovementScript.isMoving)
        {
            KnockbackEnemies();

        }
    }

    private void KnockbackEnemies()
    {
        foreach (Knockback_Enemy enemy in enemiesInRange)
        {
            if (enemy == null)
            {
                enemiesInRange.Remove(enemy);
            }
            else
            {
                //left
                if (myRigidbody2D.velocity.x < 0)
                {
                    if (enemy.collisionCollider.bounds.center.x < transform.position.x - knockbackDetectionLeeway)
                    {
                        KnockEnemyBack(enemy);
                    }
                }

                //right
                if (myRigidbody2D.velocity.x > 0)
                {
                    if (enemy.collisionCollider.bounds.center.x > transform.position.x + knockbackDetectionLeeway)
                    {
                        KnockEnemyBack(enemy);
                    }
                }

                //down
                if (myRigidbody2D.velocity.y < 0)
                {
                    if (enemy.collisionCollider.bounds.center.y < transform.position.y - knockbackDetectionLeeway)
                    {
                        KnockEnemyBack(enemy);
                    }
                }

                //up
                if (myRigidbody2D.velocity.y > 0)
                {
                    if (enemy.collisionCollider.bounds.center.y > transform.position.y + knockbackDetectionLeeway)
                    {
                        KnockEnemyBack(enemy);
                    }
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Collision"))
        {
            Knockback_Enemy enemyKnockbackScript = collision.GetComponent<Knockback_Enemy>();

            if (!enemiesInRange.Contains(enemyKnockbackScript))
            {
                enemiesInRange.Add(enemyKnockbackScript);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Collision"))
        {

            Knockback_Enemy enemyKnockbackScript = collision.GetComponent<Knockback_Enemy>();

            if (enemiesInRange.Contains(enemyKnockbackScript))
            {
                enemiesInRange.Remove(enemyKnockbackScript);
            }

        }
    }

    void KnockEnemyBack(Knockback_Enemy enemyKnockbackScript)
    {
        Vector2 knockbackDirection = enemyKnockbackScript.transform.position - transform.position;

        //if displacement is too low, enemy may get stuck on player.
        //this code knocks the enemy back with extra force to get them unstuck
        knockbackDirection = Unstick(knockbackDirection);

        enemyKnockbackScript.ReceiveKnockback(knockbackForce, knockbackDirection.normalized);
    }

    /// <summary>
    /// adds extra force to unstick enemies if they are at risk of getting stuck on the edges of the
    /// player's collider
    /// </summary>
    /// <param name="knockbackDirection"></param>
    /// <returns></returns>
    private Vector2 Unstick(Vector2 knockbackDirection)
    {
        if (knockbackDirection.x <= stuckEnemyRange)
        {

            if (knockbackDirection.x <= 0)
            {
                knockbackDirection = new Vector2(knockbackDirection.x - unstickEnemyDisplacement,
                                                knockbackDirection.y);
            }
            else
            {
                knockbackDirection = new Vector2(knockbackDirection.x + unstickEnemyDisplacement,
                                                knockbackDirection.y);
            }
        }
        if (knockbackDirection.y <= stuckEnemyRange)
        {

            if (knockbackDirection.y <= 0)
            {
                knockbackDirection = new Vector2(knockbackDirection.x,
                                                knockbackDirection.y - unstickEnemyDisplacement);
            }
            else
            {
                knockbackDirection = new Vector2(knockbackDirection.x,
                                                knockbackDirection.y + unstickEnemyDisplacement);
            }
        }

        return knockbackDirection;
    }
}
