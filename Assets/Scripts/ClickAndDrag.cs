using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private float rotateSpeed = 2.3f;

    private void FixedUpdate()
    {
        if (isDragging)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.Rotate(Vector3.forward * rotateSpeed);
            }

            if (Input.GetKey(KeyCode.E))
            {
                this.transform.Rotate(-Vector3.forward * rotateSpeed);
            }
        }
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentPosition;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
