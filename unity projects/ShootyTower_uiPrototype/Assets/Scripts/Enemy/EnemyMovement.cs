using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Vector2 targetPos;
    public float moveSpeed = 1;
    public Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //find tower position for pathfinding
        targetPos = GameObject.FindWithTag("TowerBase").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //calculate normalized velocity vector to approach tower, then multiply velocity by movespeed
        Vector2 movementVector = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        movementVector.Normalize();
        myRigidbody.velocity = movementVector * moveSpeed;
    }
}
