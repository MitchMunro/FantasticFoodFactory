using UnityEngine;

public class NoBuildZone : MonoBehaviour
{
    private int numDraggableObjectsInside;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DraggableObject"))
        {
            numDraggableObjectsInside++;

            GameManager.Instance.NoBuildZoneViolated();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DraggableObject"))
        {
            numDraggableObjectsInside--;
            if (numDraggableObjectsInside == 0)
            {
                GameManager.Instance.NoBuildZoneUnViolated();

            }
        }
    }

}