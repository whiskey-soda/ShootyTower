using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Hurtbox_World : MonoBehaviour
{
    [Header("CONFIG")]
    public HeightLevel myHeightLevel;

    PropHealth_World healthScript;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponentInParent<PropHealth_World>();
    }

    public void ReportDamage(float damageVal)
    {
        healthScript.TakeDamage(damageVal);
    }

}
