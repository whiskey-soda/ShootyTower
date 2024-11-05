using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BaseClass_Enemy))]

public class Pathfinding_Enemy : MonoBehaviour
{
    [Header("DEBUG")]
    public Transform target;

    NavMeshAgent agent;
    BaseClass_Enemy myEnemyScript;

    float slowSpeed = 1.7f;

    private void Awake()
    {
        myEnemyScript = GetComponent<BaseClass_Enemy>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        ResetMoveSpeed();

    }

    public void ResetMoveSpeed()
    {
        agent.speed = myEnemyScript.moveSpeed;
    }

    /// <summary>
    /// slows enemies if their normal move speed is above the slowed move speed,
    /// otherwise leaves them alone because theyre already struggling enough
    /// </summary>
    public void ApplySlow()
    {
        if (slowSpeed < myEnemyScript.moveSpeed)
        agent.speed = slowSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        //choose target based on base height of enemy
        switch (myEnemyScript.baseHeight)
        {
            case HeightLevel.Ground:
                target = TransformLibrary_System.instance.GroundTarget;
                break;

            case HeightLevel.Tall:
                target = TransformLibrary_System.instance.TallTarget;
                break;

            case HeightLevel.High:
                target = TransformLibrary_System.instance.HighTarget;
                break;

            case HeightLevel.Sky:
                target = TransformLibrary_System.instance.SkyTarget;
                break;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //stop moving when in range, start moving when player is out of range
        if (myEnemyScript.isAttacking)
        {
            agent.ResetPath();
            agent.avoidancePriority = 40;
        }
        else
        {
            agent.avoidancePriority = 50;
            agent.SetDestination(target.position);
        }
    }

}
