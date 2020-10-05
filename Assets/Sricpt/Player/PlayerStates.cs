using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerStates : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;
        public int addStaminaSpeed = 10;
        HealthBar healthBar;
        StaminaBar staminaBar; 
        AnimatorHandler animatorHandler;
        private void Awake() {
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
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
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayTargetAnimation("Damage_01",true);
            if(currentHealth<=0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Dead_01",true);
                //Handle Player Death
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