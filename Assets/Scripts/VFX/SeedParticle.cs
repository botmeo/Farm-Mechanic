using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SeedParticle : MonoBehaviour
{
    public static Action<Vector3[]> onSeedCollision;

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionAmount = particleSystem.GetCollisionEvents(other, collisionEvents);

        Vector3[] collisionPosition = new Vector3[collisionAmount];

        for (int i = 0; i < collisionAmount; i++)
        {
            collisionPosition[i] = collisionEvents[i].intersection;
        }

        onSeedCollision?.Invoke(collisionPosition);
    }
}
