using UnityEngine;

public class DragAndDrop2 : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Draggable"))
            {
                isDragging = true;
                offset = hit.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (isDragging)
        {
            Vector3 currentPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
