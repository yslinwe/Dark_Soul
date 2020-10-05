using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SG
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;
        void Start()
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxStamina(int maxStamina)
        {
            if(slider == null)
                slider = GetComponent<Slider>();
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }
        public void SetCurrentStamina(int currentStamina)
        {
            slider.value = currentStamina;
        }
    }
}

