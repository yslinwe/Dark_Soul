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
        public string lastAttack;
        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
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
    }
}

