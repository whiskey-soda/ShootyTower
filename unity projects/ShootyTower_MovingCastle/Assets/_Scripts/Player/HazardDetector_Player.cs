using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class HazardDetector_Player : MonoBehaviour
{
    Movement_Player movementScript;
    Health_Player healthScript;
    TowerLegs_Player legsScript;

    bool isInDamageHazard = false;
    float hazardDamagePerSecond = 0;

    [SerializeField] float hazDmgResistCoefficient = .5f;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponentInParent<Movement_Player>();
        healthScript = GetComponentInParent<Health_Player>();
        legsScript = GetComponentInParent<TowerLegs_Player>();

        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        //damage player if theyre in a damage hazard
        if (isInDamageHazard)
        {
            if (legsScript.damageEffect == HazardEffect.full)
            {
                healthScript.TakeDamage(hazardDamagePerSecond * Time.deltaTime);
            }
            else if (legsScript.damageEffect == HazardEffect.resistant)
            {
                healthScript.TakeDamage(hazardDamagePerSecond * Time.deltaTime * hazDmgResistCoefficient);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            HazardData_World hazardData = collision.GetComponent<HazardData_World>();
            ApplyHazardEffects(hazardData);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            HazardData_World hazardData = collision.GetComponent<HazardData_World>();
            RemoveHazardEffects(hazardData);
        }
    }

    private void ApplyHazardEffects(HazardData_World hazardData)
    {
        if (hazardData.slows)
        {
            movementScript.ApplySlowEffect();
        }

        if (hazardData.damages)
        {
            isInDamageHazard = true;
            hazardDamagePerSecond = hazardData.damagePerSec;
        }
    }

    private void RemoveHazardEffects(HazardData_World hazardData)
    {
        if (hazardData.slows)
        {
            movementScript.RemoveSlowEffect();
        }

        if (hazardData.damages)
        {
            isInDamageHazard = false;
            hazardDamagePerSecond = 0;
        }
    }
}
