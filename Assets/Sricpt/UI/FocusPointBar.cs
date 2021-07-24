using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SG
{
    public class FocusPointBar : MonoBehaviour
    {
        public Slider slider;
        void Start()
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxFocusPoints(float maxFocusPoints)
        {
            if(slider == null)
                slider = GetComponent<Slider>();
            slider.maxValue = maxFocusPoints;
            slider.value = maxFocusPoints;
        }
        public void SetCurrentFocusPoints(float currentFocusPoints)
        {
            slider.value = currentFocusPoints;
        }
    }
}
