using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Goal : MonoBehaviour
{
    public GameObject targetObject;
    private GameObject _previousTargetObject;
    private FoodComponent foodCompTargetObject;
    private GameObject foodIcon;
    private int scoreAmount = 5;

    public GameObject lid;

    private void Awake()
    {
        foodIcon = FindObjectInChildren("FoodIcon");
    }

    // Start is called before the first frame update
    void Start()
    {
        foodCompTargetObject = targetObject.GetComponent<FoodComponent>();
        _previousTargetObject = targetObject;
    }

    private void Update()
    {
        if (Application.isEditor)
            UpdateFoodIcon();
    }

    private void UpdateFoodIcon()
    {
        //Stop executing this function if TargetObject is null, or if TargetObject hasn't changed.
        if (targetObject == null ||
            targetObject == _previousTargetObject)
            return;

        _previousTargetObject = targetObject;

        var spriteToRender = targetObject.GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null &&
            collision.tag == "MovableObject")
        {
            ScoreAndDestroyObject(collision.gameObject);
        }
    }

    private void ScoreAndDestroyObject(GameObject scoredObject)
    {
        var foodCompScoredObject = scoredObject.GetComponent<FoodComponent>();


        if (foodCompScoredObject != null &&
            foodCompScoredObject.foodType == foodCompTargetObject.foodType)
        {
            GameManager.Instance.Score(scoreAmount);
        }
        else
        {
            Debug.Log(scoredObject + "Wrong food type!");
        }

        Destroy(scoredObject);
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

    public void OpenLid()
    {
        lid.SetActive(false);
    }

    public void CloseLid()
    {
        lid.SetActive(true);

    }

}
