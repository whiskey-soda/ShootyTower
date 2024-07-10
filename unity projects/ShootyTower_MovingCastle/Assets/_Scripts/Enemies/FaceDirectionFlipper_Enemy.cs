using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding_Enemy))]

public class FaceDirectionFlipper_Enemy : MonoBehaviour
{
    Pathfinding_Enemy myPathfindingScript;

    float initialScaleX;

    private void Awake()
    {
        myPathfindingScript = GetComponent<Pathfinding_Enemy>();
        initialScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        bool targetToTheLeft = myPathfindingScript.target.position.x < transform.position.x;

        if (targetToTheLeft)
        {
            //face left
            transform.localScale = new Vector3(-initialScaleX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            //face right (initial face direction)
            transform.localScale = new Vector3(initialScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
