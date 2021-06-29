using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        public WeaponItem unanmedWeapon;

        public List<WeaponItem> weaponsInRightHandSlots;
        public List<WeaponItem> weaponsInLeftHandSlots;
        public int currentRightWeaponIndex = 0;
        public int currentLeftWeaponIndex = 0;
        public List<WeaponItem> weaponsInventory; 
        private void Awake() {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }
        private void Start() {
            rightWeapon = unanmedWeapon;
            leftWeapon = unanmedWeapon;
        }
        public void ChangeRightWeapon()
        {
            if(weaponsInRightHandSlots.Count>0)
            {   
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
                currentRightWeaponIndex = currentRightWeaponIndex%weaponsInRightHandSlots.Count;
                if(weaponsInRightHandSlots[currentRightWeaponIndex]!=null)
                {
                    rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
                    weaponSlotManager.LoadWeaponOnSlot(rightWeapon,false);
                }
            }
            // else if(currentRightWeaponIndex == 0&&weaponsInRightHandSlots[0]==null)
            // {
            //     currentRightWeaponIndex +=1;
            // }
            // else if(currentRightWeaponIndex == 1&& weaponsInRightHandSlots[1]!=null)
            // {
            //     rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            //     weaponSlotManager.LoadWeaponOnSlot(rightWeapon,false);
            // }
            // else
            // {
            //     currentRightWeaponIndex +=1;
            // }
            // if(currentRightWeaponIndex > weaponsInRightHandSlots.Length-1)
            // {
            //     currentRightWeaponIndex = -1;
            //     rightWeapon = unanmedWeapon;
            //     weaponSlotManager.LoadWeaponOnSlot(rightWeapon,false);
            // }
        }
        public void ChangeLeftWeapon()
        {
             if(weaponsInLeftHandSlots.Count>0)
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
                currentLeftWeaponIndex = currentLeftWeaponIndex%weaponsInLeftHandSlots.Count;
                if(weaponsInLeftHandSlots[currentLeftWeaponIndex]!=null)
                {
                    leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
                    weaponSlotManager.LoadWeaponOnSlot(leftWeapon,true);
                }
            }
            // else if(currentLeftWeaponIndex == 0&&weaponsInLeftHandSlots[0]==null)
            // {
            //     currentLeftWeaponIndex +=1;
            // }
            // else if(currentLeftWeaponIndex == 1&& weaponsInLeftHandSlots[1]!=null)
            // {
            //     leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            //     weaponSlotManager.LoadWeaponOnSlot(leftWeapon,true);
            // }
            // else
            // {
            //     currentLeftWeaponIndex +=1;
            // }
            // if(currentLeftWeaponIndex > weaponsInLeftHandSlots.Length-1)
            // {
            //     currentLeftWeaponIndex = -1;
            //     leftWeapon = unanmedWeapon;
            //     weaponSlotManager.LoadWeaponOnSlot(leftWeapon,true);
            // }
        }
    }

}
