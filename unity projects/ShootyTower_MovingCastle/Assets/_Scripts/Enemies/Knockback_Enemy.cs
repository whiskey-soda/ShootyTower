using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof (BaseClass_Enemy))]

public class Knockback_Enemy : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] float knockbackDecay = 10;

    Rigidbody2D myRigidbody2D;
    BaseClass_Enemy myEnemyScript;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myEnemyScript = GetComponent<BaseClass_Enemy>();
    }


    // Update is called once per frame
    void Update()
    {
        float currentXVelocity = myRigidbody2D.velocity.x;
        float currentYVelocity = myRigidbody2D.velocity.y;

        currentXVelocity = ApplyKnockbackDecay(currentXVelocity);
        currentYVelocity = ApplyKnockbackDecay(currentYVelocity);

        myRigidbody2D.velocity = new Vector2(currentXVelocity, currentYVelocity);
    }

    private float ApplyKnockbackDecay(float currentVelocity)
    {
        if (Mathf.Abs(currentVelocity) > 0)
        {

            float prospectiveVelocity = Mathf.Abs(currentVelocity) - knockbackDecay * Time.deltaTime;

            //clamp at .5 as lowest knockback velocity
            if (prospectiveVelocity <= .5)
            {
                currentVelocity = 0;
            }
            else
            {
                //set correct sign and assign new velocity to variable
                prospectiveVelocity *= Math.Sign(currentVelocity);
                currentVelocity = prospectiveVelocity;
            }
        }

        return currentVelocity;
    }

    public void ReceiveKnockback(float knockbackValue, Vector2 normalizedMovementVector)
    { 
        myRigidbody2D.velocity = (myEnemyScript.moveSpeed + knockbackValue) * normalizedMovementVector;
    }

}
