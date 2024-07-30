using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPCollector_Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("XP Orb"))
        {
            float xpValue = collision.GetComponent<Data_XP>().value;
            Leveling_Player.Instance.AddXp(xpValue);
            Destroy(collision.gameObject);
        }
    }
}
