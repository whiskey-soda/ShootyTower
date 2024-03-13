using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDPS = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("TowerBase"))
        {
            collision.GetComponent<TowerGM>().towerCurrentHealth -= (attackDPS * Time.deltaTime);
        }
    }
}
