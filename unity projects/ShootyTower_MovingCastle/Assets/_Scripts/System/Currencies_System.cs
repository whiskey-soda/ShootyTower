using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currencies_System : MonoBehaviour
{

    public static Currencies_System instance;

    public uint mineralCount = 0;
    public uint crystalCount = 0;
    public uint dustCount = 0;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this);
        }
    }
}
