using UnityEngine;

public class DeleteOnDrop : MonoBehaviour
{
    private GameObject objectBeingDragged;

    private void Start()
    {
        objectBeingDragged = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.transform.gameObject.GetComponent<ProductionLineObject>() != null)
            {
                objectBeingDragged = hit.transform.gameObject;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectBeingDragged != null && GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                Destroy(objectBeingDragged);
            }
            objectBeingDragged = null;
        }
    }
}
