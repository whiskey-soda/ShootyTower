using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(DamageNumberSpawner_System))]

//deals damage to the enemy
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
    DamageNumberSpawner_System damageNumberSpawner;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;

        damageNumberSpawner = GetComponent<DamageNumberSpawner_System>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the collision is a hurtbox.
        //if collision is a hurtbox, checks of it matches the height level of the projectile.

        if (collision.gameObject.CompareTag("Enemy Hurtbox"))
        {
            Hurtbox_Enemy hurtboxScript = collision.GetComponent<Hurtbox_Enemy>();
            bool hitSuccess = CheckForMatchingHeightLevels(hurtboxScript);
            if (hitSuccess) { ProcessEnemyHit(hurtboxScript); }

        }
        else if (collision.gameObject.CompareTag("Prop Hurtbox"))
        {
            Hurtbox_World hurtboxScript = collision.GetComponent<Hurtbox_World>();
            bool hitSuccess = CheckForMatchingHeightLevels(hurtboxScript);
            if (hitSuccess)
            {
                hurtboxScript.ReportDamage(damage);
                ProcessHit(); 
            }
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

    private bool CheckForMatchingHeightLevels(Hurtbox_Enemy hurtboxScript)
    {
        bool bulletHit = false;
        if (myHeightLevel == hurtboxScript.myHeightLevel)
        {
            bulletHit = true;
        }

        return bulletHit;
    }

    private bool CheckForMatchingHeightLevels(Hurtbox_World hurtboxScript)
    {
        bool bulletHit = false;
        if (myHeightLevel == hurtboxScript.myHeightLevel)
        {
            bulletHit = true;
        }

        return bulletHit;
    }

    /// <summary>
    /// damages an enemy and applies knockback to them, then processes the hit logic for the projectile
    /// </summary>
    /// <param name="hurtboxScript"></param>
    private void ProcessEnemyHit(Hurtbox_Enemy hurtboxScript)
    {
        hurtboxScript.ApplyKnockback(knockback, normalizedMovementVector);
        hurtboxScript.TakeDamage(damage);

        ProcessHit();
        
    }

    /// <summary>
    /// hit logic for the projectile. includes values and functions held on the projectile, such as
    /// spawning damage numbers and decrementing pierce value on hit
    /// </summary>
    public void ProcessHit()
    {
        damageNumberSpawner.SpawnDamageNumber(damage);

        pierce -= 1;
        if (pierce <= 0) { Destroy(gameObject); }

    }


}
