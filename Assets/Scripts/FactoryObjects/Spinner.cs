using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : FactoryObject
{
    public float rotateSpeed = -8;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!rb) return;

        rb.MoveRotation(rb.rotation + rotateSpeed);
    }
}
