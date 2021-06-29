using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;
        WeaponHolderSlot backSlot;
        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;
        Animator animator;
        QuickSlotsUI quickSlotsUI;
        PlayerStates playerStates;
        InputHandler inputHandler;
        public WeaponItem attackingWeapon;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerStates = GetComponentInParent<PlayerStates>();
            inputHandler = FindObjectOfType<InputHandler>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
                else if(weaponSlot.isBackSlot)
                {
                    backSlot = weaponSlot;
                }
            }
        }
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeaponItem = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(isLeft,weaponItem);
                #region Handle Left Weapon Idle Animations
                if(weaponItem != null)
                {
                    animator.CrossFade(weaponItem.left_hand_idle,0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty",0.2f);
                }
                #endregion
            }
            else
            {
                if(inputHandler.two_handFlag)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeaponItem);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    animator.CrossFade(weaponItem.two_hand_idle,0.2f);
                }
                else
                {
                    backSlot.UnloadWeaponAndDestroy();
                    animator.CrossFade("Both Arm Empty",0.2f);
                    #region Handle Right Weapon Idle Animations
                    if(weaponItem != null)
                    {
                        animator.CrossFade(weaponItem.right_hand_idle,0.2f);
                    }
                    else
                    {
                        animator.CrossFade("Right Arm Empty",0.2f);
                    }
                    #endregion
                }
                rightHandSlot.currentWeaponItem = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(isLeft,weaponItem);
        
            }
        }
        #region Handle Weapon's Damage Collider
        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        public void OpenRightDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        public void OpenLeftDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }
        public void CloseRightHandDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }
        public void CloseLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
        #endregion
        #region Handle Weapon's Stamina Drainage
        public void DrainStaminaLightAttack()
        {
            playerStates.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina*attackingWeapon.lightAttackMultiplier));
        }
        public void DrainStaminaHeavyAttack()
        {
            playerStates.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina*attackingWeapon.heavyAttackMultiplier));
        }
        #endregion
    }
}


