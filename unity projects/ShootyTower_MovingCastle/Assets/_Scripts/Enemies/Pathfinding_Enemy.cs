using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BaseClass_Enemy))]

public class Pathfinding_Enemy : MonoBehaviour
{

    [SerializeField] Transform target;

    NavMeshAgent agent;
    BaseClass_Enemy myEnemyScript;

    float heightOffset = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        myEnemyScript = GetComponent<BaseClass_Enemy>();

        //check lowest and heighest first.
        //all ground units will have ground
        //all aerial units will have aerial, but might also have other layers. aerial is the one that matters for targeting
        if (myEnemyScript.heightLevelList.Contains(HeightLevel.Ground1))
        {
            heightOffset = 0;
        }
        else if (myEnemyScript.heightLevelList.Contains(HeightLevel.Aerial))
        {
            heightOffset = 3.3f;
        }

        else if (myEnemyScript.heightLevelList.Contains(HeightLevel.Tall))
        {
            heightOffset = 1.4f;
        }
        else if (myEnemyScript.heightLevelList.Contains(HeightLevel.VeryTall))
        {
            heightOffset = 2.4f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //acquire target
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position + new Vector3(0, heightOffset));
    }

}
