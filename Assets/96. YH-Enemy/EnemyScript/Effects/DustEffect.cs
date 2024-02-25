using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    ParticleSystem dustParticles;

    
    void Start()
    {
        dustParticles = GetComponent<ParticleSystem>();
    }

    
    public void PlayEffect()
    {
        dustParticles.Play();
    }

    
}
