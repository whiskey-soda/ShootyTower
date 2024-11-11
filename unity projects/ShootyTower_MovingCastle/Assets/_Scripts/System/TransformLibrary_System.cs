using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLibrary_System : MonoBehaviour
{
    public Transform GroundTarget;
    public Transform TallTarget;
    public Transform HighTarget;
    public Transform SkyTarget;
    [Space]

    public Transform GroundWeapon;
    public Transform TallWeapon;
    public Transform HighWeapon;
    public Transform SkyWeapon;


    public static TransformLibrary_System instance;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
