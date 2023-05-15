using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Apple : Food
{
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        CombineFood(collision, FoodType.Sugar, "AppleFilling");
    }
}
