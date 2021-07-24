using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  SG
{
    public class CharcterStats : MonoBehaviour
    {
         public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;
        public int focusPontsLevel =10;
        public float currentFocusPoints;
        public float maxFocusPoints;
        public bool isDead = false;
    }

}
