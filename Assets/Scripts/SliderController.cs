using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Image sliderFill;

    public Color colorSlow = Color.green;
    public Color colorFast = Color.red;

    public void change(float value)
    {
        sliderFill.color = Color.Lerp(colorSlow, colorFast, value);
    }
}