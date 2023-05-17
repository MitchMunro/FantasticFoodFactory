using UnityEngine;

public class AddBorderTag : MonoBehaviour
{
    [SerializeField] private string tagToAdd;

    private void OnTransformChildrenChanged()
    {
        foreach (Transform child in transform)
        {
            if (!child.CompareTag(tagToAdd))
            {
                child.tag = tagToAdd;
            }
        }
    }
}


