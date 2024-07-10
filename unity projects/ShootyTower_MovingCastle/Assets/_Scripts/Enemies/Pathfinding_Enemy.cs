using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BaseClass_Enemy))]
[RequireComponent(typeof(NavMeshObstacle))]

public class Pathfinding_Enemy : MonoBehaviour
{

    [SerializeField] Transform target;

    NavMeshAgent agent;
    BaseClass_Enemy myEnemyScript;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myEnemyScript = GetComponent<BaseClass_Enemy>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //check lowest and heighest first.
        //all ground units will have ground
        //all aerial units will have aerial, but might also have other layers. aerial is the one that matters for targeting
        if (myEnemyScript.heightLevelList.Contains(HeightLevel.Ground1))
        {
            target = GameObject.FindGameObjectWithTag("Ground Target").transform;
        }
        else if (myEnemyScript.heightLevelList.Contains(HeightLevel.Aerial))
        {
            target = GameObject.FindGameObjectWithTag("Aerial Target").transform;
        }

        else if (myEnemyScript.heightLevelList.Contains(HeightLevel.Tall))
        {
            target = GameObject.FindGameObjectWithTag("Tall Target").transform;
        }
        else if (myEnemyScript.heightLevelList.Contains(HeightLevel.VeryTall))
        {
            target = GameObject.FindGameObjectWithTag("Very Tall Target").transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //stop moving when in range, start moving when player is out of range
        if (myEnemyScript.isAttacking)
        {
            agent.enabled = false;
        }
        else
        {
            agent.enabled = true;

            agent.SetDestination(target.position);
        }
    }

}
