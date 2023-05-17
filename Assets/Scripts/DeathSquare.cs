using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSquare : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);

        if (collision.collider.tag == "ScoringObject")
        {
            Destroy(collision.gameObject);
        }
    }
}
