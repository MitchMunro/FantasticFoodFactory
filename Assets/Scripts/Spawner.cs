using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public float spawnRate = 0.8f;
    public SpriteRenderer sprite;
    public bool isBlue = false;
    private bool isFactoryPlaying;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BallSpawn());
        // Set the sprite color to red when the game starts
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
    }

    private void Update()
    {
        // Toggle the sprite color between red and green when clicked
        if (GameManager.Instance.isFactoryPlaying)
        {
            if (isBlue) sprite.color = Color.blue;
            else sprite.color = Color.green;
        }
        else
        {
            sprite.color = Color.red;
        }
    }

    public IEnumerator BallSpawn()
    {
        if (GameManager.Instance.isFactoryPlaying) Instantiate(
            ball,
            this.gameObject.transform.position,
            ball.transform.rotation,
            GameManager.Instance.FoodSpawnedParent.transform);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(BallSpawn());

    }
}



