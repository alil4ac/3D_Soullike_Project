using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreathParticle : MonoBehaviour
{
    ParticleSystem[] particles;


    

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }
    

    void StartBreath()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
    }

    void StopBreath()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
        }
    }

    
}
