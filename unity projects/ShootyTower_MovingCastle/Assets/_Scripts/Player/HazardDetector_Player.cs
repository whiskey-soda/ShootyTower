using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class HazardDetector_Player : MonoBehaviour
{
    Movement_Player movementScript;
    Health_Player healthScript;

    bool isInDamageHazard = false;
    float hazardDamagePerSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponentInParent<Movement_Player>();
        healthScript = GetComponentInParent<Health_Player>();

        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        //damage player if theyre in a damage hazard
        if ( isInDamageHazard )
        {
            healthScript.TakeDamage(hazardDamagePerSecond * Time.deltaTime);
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
