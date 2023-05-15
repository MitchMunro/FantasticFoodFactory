using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Egg : Food
{
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        CombineFood(collision, FoodType.Milk, "Custard");
    }
}

public class Egg : Food_level4
{
    private new void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
