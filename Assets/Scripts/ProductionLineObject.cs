using UnityEngine;

public class ProductionLineObject : MonoBehaviour
{
    public int sellPrice;

    public void SellObject()
    {
        GameManager.Instance.UpdateScore(sellPrice);
        Destroy(this.gameObject);
    }
}
