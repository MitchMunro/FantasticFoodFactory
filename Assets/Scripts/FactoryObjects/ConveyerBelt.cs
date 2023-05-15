using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : FactoryObject
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
}
