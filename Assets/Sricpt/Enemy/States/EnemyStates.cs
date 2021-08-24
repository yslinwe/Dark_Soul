using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class EnemyStates : CharcterStats
    {
        Animator animator;
        // public HealthBar healthBar;
        EnemyAninmatorManager animatorHandler;
        private void Awake()
        {
            animatorHandler = GetComponentInChildren<EnemyAninmatorManager>();
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
        public void TakeDamageNoAnimation(int damage)
        {
             if(isDead)
                return;
            currentHealth -= damage;
            if(currentHealth<=0)
            {
                currentHealth = 0;
                isDead = true;
                //Handle Player Death
            }
        }
        public void TakeDamage(int damage)
        {
            if(isDead)
                return;
            currentHealth -= damage;
            animatorHandler.PlayTargetAnimation("Damage_01",true);
            if(currentHealth<=0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dead_01",true);
                isDead = true;
                //Handle Player Death
            }
        }

       
    }

}