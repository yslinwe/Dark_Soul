using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerStates : CharcterStats
    {
        public int addStaminaSpeed = 100;
        PlayerManager playerManager;
        HealthBar healthBar;
        StaminaBar staminaBar; 
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public float staminaRegenerationAmount = 30;
        private float staminaRegenTimer = 0;
        private void Awake() {
            playerManager = GetComponent<PlayerManager>();
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
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        
        public void TakeDamge(int damage)
        {
            if(playerManager.isInvulerable)
                return;
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
        private float SetMaxStaminaFromHealthLevel()
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
        public void RegenerateStamina()
        {
            if(playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
                if(currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
        public void HealPlayer(int healAmount)
        {
            currentHealth = currentHealth + healAmount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetCurrentHealth(currentHealth);
        }
    }

}