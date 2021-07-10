using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerStates : CharcterStats
    {
        public int addStaminaSpeed = 100;
        HealthBar healthBar;
        StaminaBar staminaBar; 
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        private void Awake() {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            
            maxStamina = SetMaxStaminaFromHealthLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }
        void Update() {
            HandleAddStamina();
        }
        private void HandleAddStamina()
        {
             if(currentStamina<maxStamina)
            {
                currentStamina += Mathf.RoundToInt(Time.deltaTime*addStaminaSpeed);
                staminaBar.SetCurrentStamina(currentStamina);
            }
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        
        public void TakeDamge(int damage)
        {
            if(isDead)
                return;
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);
            if(inputHandler.two_handFlag)
            {
                animatorHandler.PlayTargetAnimation("Damage_02",true);
                if(currentHealth<=0)
                {
                    currentHealth = 0;
                    animatorHandler.PlayTargetAnimation("Dead_02",true);
                    isDead = true;
                    //Handle Player Death
                }
            }
            else
            {
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
        private int SetMaxStaminaFromHealthLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;
            if(currentStamina<=0)
                currentStamina = 0;
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }

}