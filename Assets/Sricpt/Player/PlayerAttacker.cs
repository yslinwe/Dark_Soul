using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        PlayerManager playerManager;
        PlayerStates playerStates;
        PlayerInventory playerInventory;
        public string lastAttack;
        
        private void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerStates = GetComponentInParent<PlayerStates>();
            inputHandler = GetComponentInParent<InputHandler>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }
        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if(lastAttack == weapon.TH_Sword_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Sword_Attack_02,true);
                }
            }

        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            if(inputHandler.two_handFlag)
            {
                animatorHandler.PlayTargetAnimation(weapon.TH_Sword_Attack_01,true);
                lastAttack = weapon.TH_Sword_Attack_01;
            }
            else
            {
                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                lastAttack = weapon.OH_Light_Attack_1;
            }
        }
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            weaponSlotManager.attackingWeapon = weapon;
            if(inputHandler.two_handFlag)
            {
                animatorHandler.PlayTargetAnimation(weapon.TH_Sword_Attack_03,true);
                lastAttack = weapon.TH_Sword_Attack_03;
            }
            else
            {
                animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                lastAttack = weapon.OH_Heavy_Attack_1;
            }
        }
        #region Input Actions
        public void HandleRBAction(WeaponItem weaponItem)
        {
            switch (weaponItem.weaponType)
            {
                case WeaponType.isMeleeWeapon: PerformRBMeleeAction(weaponItem); break;
                case WeaponType.isSpellCaster: 
                case WeaponType.isFaithCaster:
                case WeaponType.isPyroCaster: PerformRBMagicAction(weaponItem); break;;
                default: break;
            }
        }
        #endregion
        #region  Attack Action
        private void PerformRBMeleeAction(WeaponItem weaponItem)
        {
            int needStamina = Mathf.RoundToInt(weaponItem.baseStamina*weaponItem.lightAttackMultiplier);
            if(playerStates.currentStamina>needStamina)
            if(playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(weaponItem);
                inputHandler.comboFlag = false;
            }
            else
            {
                if(playerManager.isInteracting || playerManager.canDoCombo)
                    return;
                HandleLightAttack(weaponItem);
            }
        }
        private void PerformRBMagicAction(WeaponItem weaponItem)
        {
            if(playerManager.isInteracting) 
                return;
                
            if(weaponItem.weaponType == WeaponType.isFaithCaster)
            {
                if(playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    if(playerStates.currentFocusPoints >= playerInventory.currentSpell.focusPointCost)
                    {
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStates);
                    }
                    else
                    {
                        animatorHandler.PlayTargetAnimation("Shrug",true);
                    }
                }
            }
        }
        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler,playerStates);
        }
        public void HandleHeavyAttackAction(WeaponItem weaponItem)
        {
            int needStamina = Mathf.RoundToInt(weaponItem.baseStamina*weaponItem.heavyAttackMultiplier);
            if(playerStates.currentStamina>needStamina)
            if(playerManager.isInteracting || playerManager.canDoCombo)
                return;
            if(playerManager.isInteracting!=true)
                HandleHeavyAttack(weaponItem);
        }
        #endregion
    }
}

