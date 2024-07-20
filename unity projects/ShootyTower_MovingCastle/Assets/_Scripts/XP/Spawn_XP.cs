using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Spawn_XP : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float landingYRandomness = .3f;

    [SerializeField] float spawnXVelocityMin = -.5f;
    [SerializeField] float spawnXVelocityMax = .5f;

    [SerializeField] float spawnYVelocityMin = .3f;
    [SerializeField] float spawnYVelocityMax = .5f;

    [Header("DEBUG")]
    public float landingPosY;
    [SerializeField] Collider2D pickupCollider;
    [SerializeField] Rigidbody2D myRigidBody;

    [SerializeField] bool isCollectible = false;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider2D>();
        pickupCollider.isTrigger = true;
        pickupCollider.enabled = false;

        myRigidBody = GetComponent<Rigidbody2D>();

        //spawn with random upward velocity
        myRigidBody.velocity = new Vector2(Random.Range(spawnXVelocityMin, spawnXVelocityMax),
                                            Random.Range(spawnYVelocityMin, spawnYVelocityMax));

    }

    private void Start()
    {
        landingPosY += Random.Range(-landingYRandomness, landingYRandomness);
    }

    private void Update()
    {
        if (!isCollectible)
        {
            //check if it has reached the ground
            if (transform.position.y <= landingPosY)
            {
                isCollectible = true;
                pickupCollider.enabled = true;

                myRigidBody.gravityScale = 0;
                myRigidBody.velocity = Vector3.zero;
            }
        }
    }

}
