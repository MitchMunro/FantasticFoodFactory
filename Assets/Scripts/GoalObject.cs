using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GoalObject : MonoBehaviour
{
    public GameObject targetObject;
    private GameObject _previousTargetObject;
    private Food foodCompTargetObject;
    private GameObject foodIcon;
    public GameObject scoreNumberEffectObject;
    private GameObject scoringNumbers;

    private void Awake()
    {
        foodIcon = FindObjectInChildren("FoodIcon");
        scoringNumbers = FindObjectInChildren("ScoringNumbers");
    }

    // Start is called before the first frame update
    void Start()
    {
        foodCompTargetObject = targetObject.GetComponent<Food>();
        _previousTargetObject = targetObject;
    }

    private void Update()
    {
        if (Application.isEditor &&
            !Application.isPlaying)
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
        var food = scoredObject.GetComponent<Food>();

        if (food != null &&
            food.foodType == foodCompTargetObject.foodType)
        {
            GameManager.Instance.UpdateScore(food.scoreValue);
            GameManager.Instance.uIManager.PlayScoreBurst1();
            TriggerScoreNumberEffect(food.scoreValue);
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

    public void TriggerScoreNumberEffect(int score)
    {
        GameObject effect = Instantiate(scoreNumberEffectObject, scoringNumbers.transform);
        ScoreNumberEffect effectScript = effect.GetComponent<ScoreNumberEffect>();

        effectScript.StartEffect(score);
    }

}
