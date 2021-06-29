using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{

    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;
        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;
        Animator animator;
        //QuickSlotsUI quickSlotsUI;
        //PlayerStates playerStates;
        //public WeaponItem attackingWeapon;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            //quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            //playerStates = GetComponentInParent<PlayerStates>();

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
            }
        }
        public void LoadWeaponOnSlot(bool isLeft)
        {
            if (isLeft)
            {
                //leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                //#region Handle Left Weapon Idle Animations
                //if (weaponItem != null)
                //{
                //    animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                //}
                //else
                //{
                //    animator.CrossFade("Left Arm Empty", 0.2f);
                //}
                //#endregion
            }
            else
            {
                //rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                //#region Handle Right Weapon Idle Animations
                //if (weaponItem != null)
                //{
                //    animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                //}
                //else
                //{
                //    animator.CrossFade("Right Arm Empty", 0.2f);
                //}
                //#endregion
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
            //playerStates.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }
        public void DrainStaminaHeavyAttack()
        {
            //playerStates.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }
        #endregion
    }
}