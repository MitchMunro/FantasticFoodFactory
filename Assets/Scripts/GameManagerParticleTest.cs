using UnityEngine;

public class GameManagerParticleTest : MonoBehaviour
{
    public ExplosionController explosionController1; // Reference to the ExplosionController script
    public ExplosionController explosionController2; // Reference to the ExplosionController script


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed.");
            explosionController1.TriggerExplosion(); // Trigger the explosion when the Space key is pressed
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("'B' pressed.");
            explosionController2.TriggerExplosion(); // Trigger the explosion when the Space key is pressed
        }
    }
}
