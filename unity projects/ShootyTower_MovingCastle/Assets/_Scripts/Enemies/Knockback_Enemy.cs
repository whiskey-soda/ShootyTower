using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider2D))]

//should be placed on enemy collision child object
public class Knockback_Enemy : MonoBehaviour
{
        
    [Header("DEBUG")]
    //allows tower to apply knockback
    public Collider2D collisionCollider;
    NavMeshAgent agent;
    BaseClass_Enemy myEnemyScript;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        myEnemyScript = GetComponentInParent<BaseClass_Enemy>();
        collisionCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// normalizes knockback direction, then knocks enemy back with the force of knockbackValue in knockbackDirection
    /// </summary>
    /// <param name="knockbackValue"></param>
    /// <param name="knockbackDirection"></param>
    public void ReceiveKnockback(float knockbackValue, Vector2 knockbackDirection)
    { 
        agent.velocity = (myEnemyScript.moveSpeed + knockbackValue) * knockbackDirection.normalized;
    }

}
