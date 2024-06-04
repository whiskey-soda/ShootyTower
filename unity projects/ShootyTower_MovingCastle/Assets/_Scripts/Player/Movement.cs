using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;

    PlayerControls playerControls;
    Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        playerControls = new PlayerControls();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = playerControls.Gameplay.Move.ReadValue<Vector2>();
        moveVector.Normalize();
        myRigidbody2D.velocity = moveVector * moveSpeed;
    }
}
