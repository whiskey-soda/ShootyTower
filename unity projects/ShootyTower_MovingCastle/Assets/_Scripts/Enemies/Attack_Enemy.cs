using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

/// <summary>
/// detects the tower's collision boxes on its own layer, and damages the player while it is in range
/// </summary>
public class Attack_Enemy : MonoBehaviour
{

    BaseClass_Enemy myBaseClass;

    private void Awake()
    {
        myBaseClass = GetComponentInParent<BaseClass_Enemy>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //damage player
        if (collision.gameObject.CompareTag("Tower Hurtbox"))
        {
            myBaseClass.isAttacking = true;
            Health_Player.instance.TakeDamage(myBaseClass.damagePerSec * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower Hurtbox"))
        {
            myBaseClass.isAttacking = false;
        }
    }

}
