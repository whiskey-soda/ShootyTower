using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Hurtbox_Enemy : MonoBehaviour
{
    [Header("CONFIG")]
    public HeightLevel myHeightLevel;

    [Header("DEBUG")]
    public BaseClass_Enemy myEnemyScript;
    [SerializeField] Knockback_Enemy myKnockbackScript;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        myEnemyScript = GetComponentInParent<BaseClass_Enemy>();
        myKnockbackScript = transform.parent.GetComponentInChildren<Knockback_Enemy>();
    }

    public void TakeDamage(float damageValue)
    {
        myEnemyScript.DamageHealth(damageValue);
    }

    public void ApplyKnockback(float knockbackValue, Vector2 knockbackDirection)
    {
        myKnockbackScript.ReceiveKnockback(knockbackValue, knockbackDirection);
    }

}
