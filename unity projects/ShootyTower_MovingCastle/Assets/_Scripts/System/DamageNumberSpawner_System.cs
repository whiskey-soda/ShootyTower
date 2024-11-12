using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner_System : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float zDisplacement = -5;

    [Header("CONFIG - Set in SCRIPT only")]
    //set in SCRIPT inspector (not on an object)
    public GameObject damageNumPrefab;

    public void SpawnDamageNumber(float damageValue)
    {
        GameObject newDamageNumber = Instantiate(damageNumPrefab, transform.position, Quaternion.identity);
        newDamageNumber.transform.position = new Vector3(newDamageNumber.transform.position.x,
                                                        newDamageNumber.transform.position.y,
                                                        zDisplacement);

        newDamageNumber.GetComponent<DamageNumber_System>().damageValue = Mathf.Round(damageValue);
    }

}
