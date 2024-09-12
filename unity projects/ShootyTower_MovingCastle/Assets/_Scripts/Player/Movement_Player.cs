using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(TowerLegs_Player))]

public class Movement_Player : MonoBehaviour
{
    [SerializeField] float currentMoveSpeed;

    [SerializeField] float fastMoveSpeed = 7;
    [SerializeField] float normalMoveSpeed = 5;
    [SerializeField] float slowMoveSpeed = 3;
    [Space]
    [SerializeField] float slowedSpeed_Full = 2;
    [SerializeField] float slowedSpeed_Resistant = 2.5f;

    bool isSlowed = false;

    public bool isMoving = false;

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

        //apply slows based on the leg resistance to slowing hazards
        if (isSlowed)
        {
            if (legsScript.slowEffect == HazardEffect.full)
            {
                currentMoveSpeed = slowedSpeed_Full;
            }
            else if (legsScript.slowEffect == HazardEffect.resistant)
            {
                currentMoveSpeed = slowedSpeed_Resistant;
            }
        }

        //apply movement
        Vector2 moveVector = playerControls.Gameplay.Move.ReadValue<Vector2>();
        moveVector.Normalize();
        myRigidbody2D.velocity = moveVector * currentMoveSpeed;

        //update isMoving variable
        if (moveVector != Vector2.zero) { isMoving = true; } else { isMoving = false; }

    }

    public void ApplySlowEffect()
    {
        isSlowed = true;
    }

    public void RemoveSlowEffect()
    {
        isSlowed = false;
    }
}
