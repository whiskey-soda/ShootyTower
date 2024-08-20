using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(TowerLegs_Player))]

public class Movement_Player : MonoBehaviour
{
    [SerializeField] float currentMoveSpeed;

    [SerializeField] float fastMoveSpeed = 5;
    [SerializeField] float normalMoveSpeed = 4;
    [SerializeField] float slowMoveSpeed = 3;

    [SerializeField] float slowedSpeed_Full = 2.5f;
    [SerializeField] float slowedSpeed_Resistant = 2;

    bool isSlowed = false;

    public bool isMoving;

    PlayerControls playerControls;
    Rigidbody2D myRigidbody2D;

    TowerLegs_Player legsScript;

    private void Awake()
    {
        playerControls = new PlayerControls();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        legsScript = GetComponent<TowerLegs_Player>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        switch (legsScript.speed)
        {
            case LegSpeed.normal:
                currentMoveSpeed = normalMoveSpeed;
                break;

            case LegSpeed.slow:
                currentMoveSpeed = slowMoveSpeed;
                break;

            case LegSpeed.fast:
                currentMoveSpeed = fastMoveSpeed;
                break;
        }

        Vector2 moveVector = playerControls.Gameplay.Move.ReadValue<Vector2>();
        moveVector.Normalize();
        myRigidbody2D.velocity = moveVector * currentMoveSpeed;

        if (moveVector != Vector2.zero) { isMoving = true; } else { isMoving = false; }

    }
}
