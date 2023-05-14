using UnityEngine;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour
{
    public float movementDistance = 5f;
    public float movementSpeed = 1f;
    public float delayTime = 0f;

    private Vector3 initialPosition;
    private float timeElapsed = 0f;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (timeElapsed < delayTime)
        {
            timeElapsed += Time.deltaTime;
            return;
        }

        float t = Mathf.SmoothStep(0f, 1f, Mathf.PingPong((Time.time - delayTime) * movementSpeed, 1f));
        t = t * t; // Square the t value to adjust the interpolation curve
        float y = Mathf.Lerp(initialPosition.y, initialPosition.y + movementDistance, t);
        transform.localPosition = new Vector3(initialPosition.x, y, initialPosition.z);
    }
}