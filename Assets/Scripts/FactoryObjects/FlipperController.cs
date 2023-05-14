using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public float maxAngle = 45f;
    public float rotationSpeed = 200f;
    public float returnSpeed = 100f;

    private bool isFlipping = false;
    private bool isReturning = false;
    private float targetRotation;
    private float startingRotation;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingRotation = rb.rotation;
        targetRotation = startingRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFlipping = true;
            targetRotation = startingRotation + maxAngle;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isReturning = true;
            isFlipping = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFlipping)
        {
            float rotationThisFrame = rotationSpeed * Time.fixedDeltaTime;
            if (Mathf.Abs(rb.rotation - targetRotation) < rotationThisFrame)
            {
                rb.rotation = targetRotation;
            }
            else
            {
                rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, targetRotation, rotationThisFrame));
            }
        }
        else if (isReturning)
        {
            float rotationThisFrame = returnSpeed * Time.fixedDeltaTime;
            if (Mathf.Abs(rb.rotation - startingRotation) < rotationThisFrame)
            {
                rb.rotation = startingRotation;
                isReturning = false;
            }
            else
            {
                rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, startingRotation, rotationThisFrame));
            }
        }
    }
}
