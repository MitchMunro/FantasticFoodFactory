using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodComponent : MonoBehaviour
{
    public FoodType foodType;

}

public enum FoodType
{
    Bun,
    Burger,
    Steak
}
