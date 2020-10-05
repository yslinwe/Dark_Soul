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
        CapsuleCollider capCol;
        Rigidbody rigid;
        Animator animator;
        // public HealthBar healthBar;
        // AnimatorHandler animatorHandler;
        private void Awake()
        {
            //animatorHandler = GetComponentInChildren<AnimatorHandler>();
            animator = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            capCol = GetComponent<CapsuleCollider>();
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
            
            //animatorHandler.PlayTargetAnimation("Damage_01",true);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Dead_01");
                Destroy(rigid);
                Destroy(capCol);
                //animatorHandler.PlayTargetAnimation("Dead_01",true);
                //Handle Player Death
            }
            else
            {
                animator.Play("Damage_01");
            }
        }
    }

}