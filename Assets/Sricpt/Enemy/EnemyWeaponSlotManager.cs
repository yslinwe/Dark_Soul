using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{

    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;
        public List<WeaponItem> rightWeapon;
        public List<WeaponItem> leftWeapon;
        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;
        Animator animator;
        // //QuickSlotsUI quickSlotsUI;
        // //PlayerStates playerStates;
        // //public WeaponItem attackingWeapon;
        private void Awake()
        {
            //quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            //playerStates = GetComponentInParent<PlayerStates>();
            animator = GetComponent<Animator>();
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
        private void Start() {
            LoadWeaponOnSlot(leftWeapon[0],true);
            LoadWeaponOnSlot(rightWeapon[0],false);
        }
        public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeaponItem = weapon;
                leftHandSlot.LoadWeaponModel(weapon);
                LoadLeftWeaponDamageCollider();
                animator.CrossFade(weapon.left_hand_idle,0.2f);
            }
            else
            {
                rightHandSlot.currentWeaponItem = weapon;
                rightHandSlot.LoadWeaponModel(weapon);
                LoadRightWeaponDamageCollider();
                animator.CrossFade(weapon.right_hand_idle,0.2f);
            }
        }
        #region Handle Weapon's Damage Collider
        public void OpenDamageCollider()
        {
            bool isLeft = animator.GetBool("isLeft");
            bool isRight = animator.GetBool("isRight");
            if(isRight)
            {
                OpenRightDamageCollider();
            }
            else if(isLeft)
            {
                OpenLeftDamageCollider();
            }
        }
        public void CloseDamageCollider()
        {
            bool isLeft = animator.GetBool("isLeft");
            bool isRight = animator.GetBool("isRight");
            if(isRight)
            {
                CloseRightHandDamageCollider();
            }
            else if(isLeft)
            {
                CloseLeftHandDamageCollider();
            }
        }
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