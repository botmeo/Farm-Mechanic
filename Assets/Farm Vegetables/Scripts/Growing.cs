using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentinel.FarmVegetables
{
    public class Growing : MonoBehaviour
    {
        public bool isAnimation = true;
        public float animationSpeed = 1f;
        public bool randomize = true;
        [SerializeField] private GameObject _growingFX;
        private GameObject[] _vegetables;
        private int _step;
        private int _oldStep;

        private void Awake()
        {
            FindVegetables();
            SetActiveVegetables(0, false);

            if (randomize)
                Randomize();
        }

        // Compresses the incoming number between 0 and 1. Activates the object corresponding to the current time.
        public void SetGrowing(float time)
        {
            float part = 1f / (float)(_vegetables.Length - 1);
            _step = Mathf.RoundToInt(time / part);

            if (_oldStep != _step)
            {
                _oldStep = _step;
                SetActiveVegetables(_step, isAnimation);
            }
        }

        // Randomly returns the angle and size.
        void Randomize ()
        {
            foreach (GameObject item in _vegetables)
            {
                item.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
                item.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }
        }

        // Transition with or without animation
        void SetActiveVegetables (int index,bool animation)
        {
            if (!animation)
            {
                foreach (var item in _vegetables)
                {
                    item.SetActive(false);
                }
                _vegetables[index].SetActive(true);
                return;
            }

            if(c_GrowingAnimation != null)
            {
                StopCoroutine(c_GrowingAnimation);
                c_GrowingAnimation = StartCoroutine(GrowingAnimation(index));
            }
            else
            {
                c_GrowingAnimation = StartCoroutine(GrowingAnimation(index));
            }
        }

        // Transition animation to different steps
        Coroutine c_GrowingAnimation;
        IEnumerator GrowingAnimation (int index)
        {
            while (transform.localScale != Vector3.zero)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, animationSpeed * Time.fixedDeltaTime);
                yield return null;
            }
            foreach (var item in _vegetables)
            {
                item.SetActive(false);
            }
            // Activates if there is a particle
            if (_growingFX != null)
            {
                GameObject fx = Instantiate(_growingFX, transform);
                Destroy(fx, 3f);
            }
            _vegetables[index].SetActive(true);
            while (transform.localScale != Vector3.one)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, animationSpeed * Time.fixedDeltaTime);
                yield return null;
            }
        }

        // Lists all vegetables
        void FindVegetables ()
        {
            List<GameObject> result = new List<GameObject>();
            foreach (Transform item in transform)
            {
                result.Add(item.gameObject);
            }
            _vegetables = result.ToArray();
        }
    }
}