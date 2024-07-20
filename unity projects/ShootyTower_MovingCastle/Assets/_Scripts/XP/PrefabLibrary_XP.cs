using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLibrary_XP : MonoBehaviour
{
    //singleton
    public static PrefabLibrary_XP Instance;

    [SerializeField] public GameObject BasicXPOrb;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
}
