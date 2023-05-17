using System;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public static Action<Vector3[]> onWaterCollision;

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

        onWaterCollision?.Invoke(collisionPosition);
    }
}
