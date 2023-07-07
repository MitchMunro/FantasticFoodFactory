using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public ParticleSystem explosionParticle; // Reference to the Particle System

    public void TriggerExplosion()
    {
        explosionParticle.Play(); // Play the Particle System
    }
}
