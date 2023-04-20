using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBallStop : MonoBehaviour
{
    private float dampenBounceRate = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);

        if (collision.collider.tag == "ScoringObject")
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity =
                new Vector2(
                rb.velocity.x / dampenBounceRate,
                rb.velocity.y / dampenBounceRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
