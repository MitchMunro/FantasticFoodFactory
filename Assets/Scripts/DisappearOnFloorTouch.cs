using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnFloorTouch : MonoBehaviour
{
    public float fadeTime = 3.0f; // time it takes for the object to fade out
    private bool touchingFloor = false; // flag to indicate if the object is currently touching the floor
    private float timeSinceTouchingFloor = 0.0f; // time since the object started touching the floor
    private SpriteRenderer objectRenderer; // the object's sprite renderer component
    private Color originalColor; // the object's original color

    // Start is called before the first frame update
    void Start()
    {
        // get the object's sprite renderer component
        objectRenderer = GetComponent<SpriteRenderer>();
        // get the object's original color
        originalColor = objectRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingFloor)
        {
            // increment the time since the object started touching the floor
            timeSinceTouchingFloor += Time.deltaTime;

            // calculate the alpha value for the object's material based on the fade time and the time since the object started touching the floor
            float alpha = Mathf.Lerp(1.0f, 0.0f, timeSinceTouchingFloor / fadeTime);

            // set the alpha value for the object's material
            Color color = objectRenderer.color;
            color.a = alpha;
            objectRenderer.color = color;

            // if the alpha value is less than or equal to 0, destroy the object
            if (alpha <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // calculate the alpha value for the object's material based on the fade time and the time since the object stopped touching the floor
            float alpha = Mathf.Lerp(0.0f, 1.0f, Time.timeSinceLevelLoad / fadeTime);

            // set the alpha value for the object's material
            Color color = objectRenderer.color;
            color.a = alpha;
            objectRenderer.color = color;
        }
    }

    // called when the object collides with another object
    void OnCollisionEnter2D(Collision2D collision)
    {
        // check if the other object has the "Floor" tag
        if (collision.gameObject.tag == "Floor")
        {
            // set the touchingFloor flag to true and reset the time since the object started touching the floor
            touchingFloor = true;
            timeSinceTouchingFloor = 0.0f;
        }
    }

    // called when the object stops colliding with another object
    void OnCollisionExit2D(Collision2D collision)
    {
        // check if the other object has the "Floor" tag
        if (collision.gameObject.tag == "Floor")
        {
            // set the touchingFloor flag to false
            touchingFloor = false;
        }
    }
}