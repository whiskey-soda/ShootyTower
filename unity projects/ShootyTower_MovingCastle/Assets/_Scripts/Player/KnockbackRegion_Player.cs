using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class KnockbackRegion_Player : MonoBehaviour
{
    

    [Header("CONFIG")]
    [SerializeField] HeightLevel myHeightLevel;
    [SerializeField] Direction myDirection;
    [SerializeField] float knockbackForce = 5;

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
            bool isActive = CheckIfActive();

            if (isActive)
            {
                foreach (Knockback_Enemy enemy in enemiesInRange)
                {
                    KnockEnemyBack(enemy);
                }
            }

        }
    }

    private bool CheckIfActive()
    {
        bool isActive = false;
        switch (myDirection)
        {
            case Direction.Left:
                if (myRigidbody2D.velocity.x < 0) { isActive = true; }
                break;

            case Direction.Right:
                if (myRigidbody2D.velocity.x > 0) { isActive = true; }
                break;

            case Direction.Up:
                if (myRigidbody2D.velocity.y > 0) { isActive = true; }
                break;

            case Direction.Down:
                if (myRigidbody2D.velocity.y < 0) { isActive = true; };
                break;
        }

        return isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Collision"))
        {
            bool isCorrectHeight = false;

            BaseClass_Enemy enemyScript = collision.GetComponentInParent<BaseClass_Enemy>();
            isCorrectHeight = CheckForMatchingHeightLevel(isCorrectHeight, enemyScript);

            if (isCorrectHeight)
            {
                Knockback_Enemy enemyKnockbackScript = enemyScript.GetComponent<Knockback_Enemy>();
                if (!enemiesInRange.Contains(enemyKnockbackScript))
                {
                    enemiesInRange.Add(enemyKnockbackScript);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Collision"))
        {

            Knockback_Enemy enemyKnockbackScript = collision.GetComponentInParent<Knockback_Enemy>();
            if (enemiesInRange.Contains(enemyKnockbackScript))
            {
                enemiesInRange.Remove(enemyKnockbackScript);
            }

        }
    }

    private bool CheckForMatchingHeightLevel(bool hitDetected, BaseClass_Enemy enemyScript)
    {
        if (enemyScript != null)
        {
            foreach (HeightLevel heightLevel in enemyScript.heightLevelList)
            {
                if (heightLevel == myHeightLevel) { hitDetected = true; }
            }
        }

        return hitDetected;
    }

    void KnockEnemyBack(Knockback_Enemy enemyKnockbackScript)
    {
        Vector2 knockbackDirection = enemyKnockbackScript.transform.position - transform.position;
        enemyKnockbackScript.ReceiveKnockback(knockbackForce, knockbackDirection.normalized);
    }
}
