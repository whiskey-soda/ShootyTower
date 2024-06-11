using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BaseClass_Projectile : MonoBehaviour
{

    public float damage;
    public float speed; //meters per second
    public float pierce;
    public float knockback;
    public float range; //meters
    public ElementType element;
    public HeightLevel myHeightLevel;
    public Vector2 normalizedMovementVector;

    [SerializeField] Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BaseClass_Enemy enemyScript = collision.gameObject.GetComponent<BaseClass_Enemy>();
            bool hitSuccess = CheckForMatchingHeightLevels(enemyScript);
            if (hitSuccess) { ProcessHit(enemyScript); }

        }
    }

    /// <summary>
    /// moves bullet and checks speed variable
    /// </summary>
    private void ProcessMovement()
    {
        myRigidbody2D.velocity = normalizedMovementVector * speed;

        range -= speed * Time.deltaTime;
        if (range <= 0)
        {
            Destroy(gameObject);
        }
    }

    private bool CheckForMatchingHeightLevels(BaseClass_Enemy enemyScript)
    {
        bool bulletHit = false;
        foreach (HeightLevel enemyHeightLevel in enemyScript.heightLevelList)
        {
            if (enemyHeightLevel == myHeightLevel)
            {
                bulletHit = true;
            }
        }

        return bulletHit;
    }

    private void ProcessHit(BaseClass_Enemy enemyScript)
    {
        enemyScript.takeDamage(damage);
        pierce -= 1;
        if (pierce <= 0) { Destroy(gameObject); }

        enemyScript.gameObject.GetComponent<Knockback_Enemy>().ReceiveKnockback(knockback, normalizedMovementVector);
    }
}
