using UnityEngine;
using System.Collections;

public class SpriteFlash : MonoBehaviour
{
    public SpriteRenderer spriteRenderer1; // Drag your sprite renderer component here in the inspector
    public SpriteRenderer spriteRenderer2;
    public float flashSpeed = 0.5f; // Adjust the speed of the flash in the inspector

    private bool isFadingOut = true;

    private void Start()
    {
        
    }

    void Update()
    {
        // Calculate the new alpha value of the sprite based on the current time and flash speed
        float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f);

        // Set the alpha value of the sprite renderer
        Color spriteColor = spriteRenderer1.color;
        spriteColor.a = alpha;
        spriteRenderer1.color = spriteColor;
        spriteRenderer2.color = spriteColor;

        // If the alpha value is currently 1 or 0, switch the fading direction
        if (alpha == 1f && isFadingOut)
        {
            isFadingOut = false;
        }
        else if (alpha == 0f && !isFadingOut)
        {
            isFadingOut = true;
        }
    }
}
