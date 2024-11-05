using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (BaseClass_Enemy))]

public class Knockback_Enemy : MonoBehaviour
{

    [Header("CONFIG")]
    //speed at which knockback force decreases per second
    [SerializeField] float knockbackDecay = 10;
    //used for detecting collisions with tower
    public Collider2D collisionCollider;

    [Header("DEBUG")]
    NavMeshAgent agent;
    BaseClass_Enemy myEnemyScript;

    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myEnemyScript = GetComponent<BaseClass_Enemy>();
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
