using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private float rotateSpeed = 1.6f;
    public int sellPrice;

    private void FixedUpdate()
    {
        if (isDragging &&
            !GameManager.Instance.isFactoryPlaying)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.Rotate(Vector3.forward * rotateSpeed);
            }

            if (Input.GetKey(KeyCode.E))
            {
                this.transform.Rotate(-Vector3.forward * rotateSpeed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                SellObject();
            }
        }
    }

    private void SellObject()
    {
        GameManager.Instance.UpdateScore(sellPrice);
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.isFactoryPlaying) return;

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        //if (GameManager.Instance.isFactoryPlaying) return;

        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Draggable"), Mathf.NegativeInfinity, Mathf.Infinity);

        //if (hit.collider != null && hit.collider.gameObject == gameObject)
        //{
        //    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    isDragging = true;
        //}
    }

    private void OnMouseDrag()
    {
        if (GameManager.Instance.isFactoryPlaying) return;

        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentPosition;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
