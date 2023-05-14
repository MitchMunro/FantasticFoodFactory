using UnityEngine;

public class FactoryObject : MonoBehaviour
{
    public int buyPrice;

    private GameObject highlightObject;
    private float highlightThickness = 0.2f;

    void Start()
    {
        CreateHighlightSprite();

    }

    private void CreateHighlightSprite()
    {
        // Get the SpriteRenderer component of the current GameObject

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers == null)
        {
            Debug.LogWarning("No SpriteRenderer found on GameObject!");
            return;
        }

        // Create a new GameObject to hold the copied SpriteRenderer
        highlightObject = new GameObject("HighlightParent");
        HighlightDeactivate();
        highlightObject.transform.SetParent(transform);
        highlightObject.transform.localPosition = Vector3.zero;

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            // You can attach the "IgnoreHighlight" tag to a sprite if you want this to not produce an on-click highlight for that specific sprite layer.
            if (renderer.gameObject.tag == "IgnoreHighlight") continue;

            GameObject highlightComponent = new GameObject("Highlight");
            highlightComponent.transform.SetParent(highlightObject.transform);

            highlightComponent.transform.localScale = renderer.gameObject.transform.localScale;
            highlightComponent.transform.localRotation = renderer.gameObject.transform.localRotation;
            highlightComponent.transform.localPosition = renderer.gameObject.transform.localPosition;

            //Calculate the how wide the sprite is so that you can add a uniform length highlight
            float xScale =  highlightThickness / renderer.sprite.bounds.size.x;
            float yScale =  highlightThickness / renderer.sprite.bounds.size.y;


            highlightComponent.transform.localPosition = Vector3.zero;
            highlightComponent.transform.localScale = new Vector3(
                highlightComponent.transform.localScale.x + xScale,
                highlightComponent.transform.localScale.y + yScale,
                1f);

            // Copy the SpriteRenderer component onto the new GameObject
            SpriteRenderer newSpriteRenderer = highlightComponent.AddComponent<SpriteRenderer>();
            newSpriteRenderer.sprite = renderer.sprite;

            var color = Color.black;
            color.a = 0.4f;
            newSpriteRenderer.color = color;

            newSpriteRenderer.flipX = renderer.flipX;
            newSpriteRenderer.flipY = renderer.flipY;
            newSpriteRenderer.sortingLayerID = renderer.sortingLayerID;
            newSpriteRenderer.sortingOrder = renderer.sortingOrder - 1;
            newSpriteRenderer.drawMode = renderer.drawMode;
            newSpriteRenderer.maskInteraction = renderer.maskInteraction;

        }
    }

    public void HighlightActivate()
    {
        if (highlightObject == null)
        {
            Debug.Log("No highlight object.");
            return;
        }

        highlightObject.SetActive(true);
    }

    public void HighlightDeactivate()
    {
        if (highlightObject == null)
        {
            Debug.Log("No highlight object.");
            return;
        }

        highlightObject.SetActive(false);
    }

    public void SellObject()
    {
        GameManager.Instance.UpdateScore(buyPrice);
        Destroy(this.gameObject);
    }
}
