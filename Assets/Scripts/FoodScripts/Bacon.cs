using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : Food_level4
{
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        var foodComponent = collision.gameObject.GetComponent<Food_level4>();

        if (foodComponent != null &&
            foodComponent.foodType_level4 == FoodType_level4.Bread)
        {
            Destroy(collision.gameObject);

            Instantiate(GameManager.Instance.sandwich,
                collision.gameObject.transform.position,
                GameManager.Instance.sandwich.transform.rotation,
                GameManager.Instance.FoodSpawnedParent.transform
                );

            Destroy(this.gameObject);
        }
    }
}
