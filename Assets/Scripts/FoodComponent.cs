using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodComponent : MonoBehaviour
{
    public FoodType foodType;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {

        }
    }

}

public enum FoodType
{
    Bun,
    Burger,
    Steak,
    Icecream
}
