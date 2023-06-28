using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public GameObject buyObject;
    public int cost = 20;
    private Transform objectsBought;

    private void Start()
    {
        objectsBought = GameManager.Instance.ObjectsBoughtParent.transform;
    }

    private void OnMouseUp()
    {
        if (!GameManager.Instance.isFactoryPlayingAtAll() &&
            GameManager.Instance.money >= cost)
        {
            GameManager.Instance.UpdateScore(-cost);
            Instantiate(buyObject,
                this.transform.position + new Vector3(1,-1,0),
                buyObject.transform.rotation,
                objectsBought) ;
        }

    }

}