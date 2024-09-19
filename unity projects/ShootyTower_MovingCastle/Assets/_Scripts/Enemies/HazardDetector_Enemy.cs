using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//uses existing ground collider to detect hazards
[RequireComponent(typeof(Collider2D))]

//to be placed on enemy ground collision child object :)
public class HazardDetector_Enemy : MonoBehaviour
{

    Pathfinding_Enemy pathfindingScript;

    // Start is called before the first frame update
    void Start()
    {
        pathfindingScript = GetComponentInParent<Pathfinding_Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            HazardData_World hazData = collision.GetComponent<HazardData_World>();
            if (hazData.slows)
            {
                pathfindingScript.ApplySlow();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            HazardData_World hazData = collision.GetComponent<HazardData_World>();
            if (hazData.slows)
            {
                pathfindingScript.ResetMoveSpeed();
            }
        }
    }
}
