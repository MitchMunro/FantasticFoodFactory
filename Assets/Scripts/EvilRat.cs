using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EvilRat : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    public float lifeTime = 15f;
    [SerializeField] private Rigidbody2D rb;
    private bool isMovingOnXAxis; 
    private Vector2 movementDirection;
    private bool isFactoryPlaying;


    void Start()
    {
            transform.position = new Vector2(Random.Range(-4f, 2f), Random.Range(-4f, 4f));

            isMovingOnXAxis = Random.Range(0, 2) == 0;
            movementDirection = isMovingOnXAxis ? Vector2.right : Vector2.up;

            Destroy(gameObject, lifeTime);
    }



    void Update()
    {
        if (GameManager.Instance.isFactoryPlaying)
        {
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);


            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "MovableObject")
                {
                    Destroy(collider.gameObject);
                }
                if (collider.tag == "Border")
                {
                    movementDirection *= -1;
                }
            }
        }
    }
}