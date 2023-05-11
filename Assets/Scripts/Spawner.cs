using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Spawner : MonoBehaviour
{
    public GameObject foodToSpawn;
    private GameObject _previousFoodToSpawn;
    public float spawnRate = 0.8f;
    private bool isFactoryPlaying;
    private GameObject foodIcon;
    public SpriteRenderer colorChangingSprite;


    private void Awake()
    {
        foodIcon = FindObjectInChildren("FoodIcon");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
            StartCoroutine(SpawnFood());
            // Set the sprite color to red when the game starts
            if (colorChangingSprite != null) colorChangingSprite.color = Color.red;
        }
    }

    private void Update()
    {
        
        if (Application.isEditor &&
            !Application.isPlaying)   //Execute this if in editor
        {
            UpdateFoodIcon();
        }
        else    //Execute this if in play mode
        {
            if (colorChangingSprite == null) return;

            // Toggle the sprite color between red and green when clicked
            if (GameManager.Instance.isFactoryPlaying)
            {
                colorChangingSprite.color = Color.green;
            }
            else
            {
                colorChangingSprite.color = Color.red;
            }
        }
            
    }

    private void UpdateFoodIcon()
    {
        //Stop executing this function if TargetObject is null, or if TargetObject hasn't changed.
        if (foodToSpawn == null ||
            foodToSpawn == _previousFoodToSpawn)
            return;

        _previousFoodToSpawn = foodToSpawn;

        var spriteToRender = foodToSpawn.GetComponent<SpriteRenderer>();
        var iconRenderer = foodIcon.GetComponent<SpriteRenderer>();

        try
        {
            iconRenderer.sprite = spriteToRender.sprite;
        }
        catch
        {
            Debug.Log("Failed to render sprite from target object.");
        }


    }

    public GameObject FindObjectInChildren(string name)
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public IEnumerator SpawnFood()
    {
        if (GameManager.Instance.isFactoryPlaying) Instantiate(
            foodToSpawn,
            this.gameObject.transform.position,
            foodToSpawn.transform.rotation,
            GameManager.Instance.FoodSpawnedParent.transform);
        yield return new WaitForSeconds(
            spawnRate / GameManager.Instance.SpeedSliderMultiplier());
        StartCoroutine(SpawnFood());

    }
}



