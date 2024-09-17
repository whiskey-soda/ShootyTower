using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class HazardData_World : MonoBehaviour
{
    public bool slows = false;
    public bool damages = false;
    public float damagePerSec = 0;

    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
