using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpDebugger_System : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] float XPAmount;

    [Header("DEBUG")]
    [SerializeField] Leveling_Player levelingScript;

    private void Start()
    {
        levelingScript = GameObject.FindObjectOfType<Leveling_Player>();
    }

    [ContextMenu("Add XP")]
    void AddWeapon()
    {
        levelingScript.AddXp(XPAmount); ;
    }
}
