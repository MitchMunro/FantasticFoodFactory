using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meatball : Food
{
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        CombineFood(collision, FoodType.Spaghetti, "SpaghettiAndMeatballs");
    }
}
