using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public GameObject particleSysGameObject;
    private ParticleSystem partSys;

    // Start is called before the first frame update
    void Start()
    {
        partSys = particleSysGameObject.GetComponent<ParticleSystem>();

        partSys.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
