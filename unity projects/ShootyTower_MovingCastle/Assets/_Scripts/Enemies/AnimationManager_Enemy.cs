using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AnimationManager_Enemy : MonoBehaviour
{
    public BaseClass_Enemy myEnemyScript;
    public Animator myAnimator;

    private void Awake()
    {
        myEnemyScript = GetComponentInParent<BaseClass_Enemy>();
        myAnimator = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (myEnemyScript.isAttacking)
        {
            myAnimator.SetBool("isAttacking", true);
            myAnimator.SetBool("isMoving", false);
        }
        else
        {
            myAnimator.SetBool("isAttacking", false );
            myAnimator.SetBool("isMoving", true );
        }
    }
}
