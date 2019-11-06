using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorParticleBehaviour : MonoBehaviour
{

    public Transform Target;

    ParticleSystem vectorSystem;

    static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[2000];

    int count;

    void Start()
    {
        if (vectorSystem == null)
            vectorSystem = GetComponent<ParticleSystem>();

        if (vectorSystem == null)
        {
            this.enabled = false;
        }
        else
        {
            vectorSystem.Play();
        }
    }
    void Update()
    {

        count = vectorSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            ParticleSystem.Particle particle = particles[i];

            Vector3 v1 = vectorSystem.transform.TransformPoint(particle.position);
            Vector3 v2 = Target.transform.position;
            
            Vector3 targetPos = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);
            //targetPos.y += 0.5f;
            particle.position = vectorSystem.transform.InverseTransformPoint(v2 - targetPos);
            particles[i] = particle;
        }

        vectorSystem.SetParticles(particles, count);
    }
}
