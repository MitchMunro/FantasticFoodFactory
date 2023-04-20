using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    public float spawnRate = 2;
    public SpriteRenderer sprite;
    private bool isOn = false;
    public bool isBlue = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BallSpawn());
        // Set the sprite color to red when the game starts
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
    }

    void OnMouseDown()
    {
        // Toggle the sprite color between red and green when clicked
        if (isOn)
        {
            sprite.color = Color.red;
            isOn = false;
        }
        else
        {
            if (isBlue) sprite.color = Color.blue;
            else sprite.color = Color.green;
            isOn = true;
        }
    }

    public IEnumerator BallSpawn()
    {
        if (isOn) Instantiate(ball, this.gameObject.transform.position, ball.transform.rotation);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(BallSpawn());

    }
}



