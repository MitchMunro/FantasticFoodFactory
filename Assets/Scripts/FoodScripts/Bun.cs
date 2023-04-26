using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bun : FoodComponent
{
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        var foodComponent = collision.gameObject.GetComponent<FoodComponent>();

        if (foodComponent != null &&
            foodComponent.foodType == FoodType.Steak)
        {
            Destroy(collision.gameObject);

            Instantiate(GameManager.Instance.burger,
                collision.gameObject.transform.position,
                GameManager.Instance.burger.transform.rotation,
                GameManager.Instance.FoodSpawnedParent.transform
                );

            Destroy(this.gameObject);
        }
    }
}
