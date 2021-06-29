using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class EnemyStates : CharcterStats
    {
        Animator animator;
        // public HealthBar healthBar;
        AnimatorHandler animatorHandler;
        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            // healthBar.SetMaxHealth(maxHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
       
    }

}