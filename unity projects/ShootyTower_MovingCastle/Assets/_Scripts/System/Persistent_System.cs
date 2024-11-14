using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent_System : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
