using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float rotateSpeed = 8;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * -1 * rotateSpeed);
    }
}
