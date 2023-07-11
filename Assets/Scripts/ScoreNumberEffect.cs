using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreNumberEffect : MonoBehaviour
{
    private float verticleSpeed = 1.6f;
    private float fadeDuration = 0.6f; // Duration of the fade-out effect in seconds

    private TextMeshPro TMPtext;
    private float currentAlpha;
    private bool effectPlaying = false;


    private void Awake()
    {
        TMPtext = GetComponent<TextMeshPro>();
        currentAlpha = TMPtext.color.a;
    }

    private void Update()
    {
        if (effectPlaying)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * verticleSpeed);

            // Reduce the alpha value gradually over time
            currentAlpha -= Time.deltaTime / fadeDuration;

            // Update the sprite renderer's color with the new alpha value
            Color newColor = TMPtext.color;
            newColor.a = currentAlpha;
            TMPtext.color = newColor;

            // Check if the object has become invisible
            if (currentAlpha <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartEffect(int scoreNumber)
    {
        TMPtext.text = $"+{scoreNumber.ToString()}";
        effectPlaying = true;
    }

}
