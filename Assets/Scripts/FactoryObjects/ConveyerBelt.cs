using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt_1 : FactoryObject
{
    public float boostRate = 20;


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "MovableObject")
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 pushDirection = transform.right;

            rb.AddForce(pushDirection * boostRate);
            Debug.Log(pushDirection * boostRate);

        }
    }

    private void FixedUpdate()
    {
        
    }

    public class RotateWithRKey : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(Vector3.forward, 90.0f);
        }
    }
}
}
