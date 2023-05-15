using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sentinel.FarmVegetables
{
    public class GrowingManagerSetUI : MonoBehaviour
    {
        public Slider slider;
        public GrowingManager growingManager;

        public void OnValueChange()
        {
            growingManager.progressTime = slider.value;
        }
    }
}