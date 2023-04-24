using UnityEngine;
using System.Collections.Generic;

public class CustomTag : MonoBehaviour
{
    [SerializeField]
    private List<string> tags = new List<string>();

    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    public IEnumerable<string> GetTags()
    {
        return tags;
    }

    public void Rename(int index, string tagName)
    {
        tags[index] = tagName;
    }

    public string GetAtIndex(int index)
    {
        return tags[index];
    }

    public int Count
    {
        get { return tags.Count; }
    }

}

public static class Extensions
{
    /// <summary>
    /// Tests if the collider has the tag, or the custom tag attached by
    /// the custom tag component. Custom Tag allows for multiple tags.
    /// </summary>
    /// <param name="collider2D"></param>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public static bool HasTag(this Component collider2D, string tagName)
    {
        bool customTag = collider2D?.GetComponentInParent<CustomTag>()?.HasTag(tagName) ?? false;

        if (collider2D.CompareTag(tagName) || customTag)
        {
            return true;
        }
        return false;
    }

    public static bool HasTag(this GameObject gameObject, string tagName)
    {
        bool customTag = gameObject?.GetComponent<CustomTag>()?.HasTag(tagName) ?? false;

        if (gameObject.CompareTag(tagName) || customTag)
        {
            return true;
        }
        return false;
    }
}