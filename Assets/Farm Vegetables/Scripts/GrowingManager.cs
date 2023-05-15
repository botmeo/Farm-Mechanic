using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentinel.FarmVegetables
{
    public class GrowingManager : MonoBehaviour
    {
        [Range(0f, 1f)] public float progressTime;
        private Growing[] growings;

        private void Awake()
        {
            FindGrowing();
        }

        private void Update()
        {
            // Send time to all objects
            foreach (Growing growing in growings)
            {
                growing.SetGrowing(progressTime);
            }
        }

        void FindGrowing()
        {
            growings = FindObjectsOfType<Growing>();
        }
    }
}