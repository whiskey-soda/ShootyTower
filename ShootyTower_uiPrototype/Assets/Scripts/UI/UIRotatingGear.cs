using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotatingGear : MonoBehaviour
{
    public bool isSpeedingUp =false;
    public bool isSlowingDown = false;

    public float rotAcceleration = 25;
    public float rotDecceleration = 2;
    public float rotSpeed = 0;
    public float maxRotSpeed = 350;
    public float minRotSpeed = 30;

    private void Start()
    {
        startRotating();
        rotSpeed = maxRotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpeedingUp)
        {
            transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
            rotSpeed = Mathf.Min( rotSpeed + rotAcceleration, maxRotSpeed );
        }

        if (isSlowingDown)
        {
            transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
            rotSpeed = Mathf.Max(rotSpeed - rotDecceleration, minRotSpeed);
        }
    }

    public void startRotating()
    {
        isSpeedingUp = true;
        isSlowingDown = false;
    }

    public void stopRotating()
    {
        isSpeedingUp = false;
        isSlowingDown = true;
    }
}
