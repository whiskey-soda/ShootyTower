using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner_System : MonoBehaviour
{

    //set in SCRIPT inspector (not on an object)
    public GameObject damageNumPrefab;

    public void SpawnDamageNumber(float damageValue)
    {
        GameObject newDamageNumber = Instantiate(damageNumPrefab, transform.position, Quaternion.identity);

        newDamageNumber.GetComponent<DamageNumber_System>().damageValue = Mathf.Round(damageValue);
    }

}
