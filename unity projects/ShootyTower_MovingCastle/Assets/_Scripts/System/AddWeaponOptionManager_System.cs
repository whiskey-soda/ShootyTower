using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeaponOptionManager_System : MonoBehaviour
{

    public static AddWeaponOptionManager_System instance;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
