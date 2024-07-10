using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshRuntimeBaking : MonoBehaviour
{

    NavMeshSurface surface;

    private void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        surface.BuildNavMesh();
    }
}
