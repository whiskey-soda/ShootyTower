using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSetup_System : MonoBehaviour
{
    //.5 is lowest recommended value
    //should help collisions
    [SerializeField] float avoidancePredictionTime = .5f; 

    // Start is called before the first frame update
    void Start()
    {
        NavMesh.avoidancePredictionTime = avoidancePredictionTime;
    }
}
