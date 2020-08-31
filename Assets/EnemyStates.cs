using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class EnemyStates : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        Animator animator;
        // public HealthBar healthBar;
        // AnimatorHandler animatorHandler;
        private void Awake()
        {
            //animatorHandler = GetComponentInChildren<AnimatorHandler>();
            animator = GetComponentInChildren<Animator>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            //healthBar.SetMaxHealth(maxHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        public void TakeDamge(int damage)
        {
            currentHealth -= damage;
            //healthBar.SetCurrentHealth(currentHealth);
            animator.Play("Damage_01");
            //animatorHandler.PlayTargetAnimation("Damage_01",true);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead_01");

                //animatorHandler.PlayTargetAnimation("Dead_01",true);
                //Handle Player Death
            }
        }
    }

}